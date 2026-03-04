using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Physics Settings")]
    public float jumpForce = 12f;
    public float gravityScale = 3f;
    
    [Header("Görseller (Sprites)")]
    public Sprite[] runSprites; 
    public Sprite jumpSprite;
    public Sprite deadSprite;

    [Header("Animasyon Ayarları")]
    public float animationSpeed = 0.15f; 

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private PolygonCollider2D polyCol;

    private bool isGrounded = true;
    private int currentRunFrame;
    private float animationTimer;

    public enum PlayerState { Run, Jump, Dead }
    public PlayerState currentState;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        polyCol = GetComponent<PolygonCollider2D>();

        rb.gravityScale = gravityScale;
        ChangeState(PlayerState.Run);
    }

    private void Update()
    {
        if (GameManager.Instance.isGameOver) return;

        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) && isGrounded)
        {
            Jump();
        }

        // Koşma animasyonu sırasında sadece görsel değişir, collider HESAPLANMAZ.
        if (currentState == PlayerState.Run && isGrounded)
        {
            HandleRunAnimation();
        }
    }

    private void HandleRunAnimation()
    {
        animationTimer += Time.deltaTime;

        if (animationTimer >= animationSpeed)
        {
            animationTimer = 0f;
            currentRunFrame = (currentRunFrame + 1) % runSprites.Length;
            spriteRenderer.sprite = runSprites[currentRunFrame];
            
            // BURASI ÖNEMLİ: Koşma kareleri birbirine çok benzediği için 
            // burada UpdateColliderShape() ÇAĞIRMIYORUZ. 
            // İlk karedeki collider ikisi için de yeterli olacaktır.
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, 0f);
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        
        isGrounded = false;
        ChangeState(PlayerState.Jump); // Zıplama görseline geçer ve collider'ı BİR KEZ günceller.
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            if (!isGrounded) // Sadece havadan yere ilk düştüğünde çalışır
            {
                isGrounded = true;
                if (!GameManager.Instance.isGameOver)
                {
                    ChangeState(PlayerState.Run); // Koşma görseline döner ve collider'ı BİR KEZ günceller.
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle") && !GameManager.Instance.isGameOver)
        {
            Die();
        }
    }

    private void Die()
    {
        ChangeState(PlayerState.Dead);
        GameManager.Instance.GameOver();
        rb.velocity = Vector2.zero;
        rb.gravityScale = 0f;
    }

    private void ChangeState(PlayerState newState)
    {
        currentState = newState;
        animationTimer = 0f;

        switch (currentState)
        {
            case PlayerState.Run:
                if (runSprites.Length > 0) spriteRenderer.sprite = runSprites[0];
                break;
            case PlayerState.Jump:
                if (jumpSprite != null) spriteRenderer.sprite = jumpSprite;
                break;
            case PlayerState.Dead:
                if (deadSprite != null) spriteRenderer.sprite = deadSprite;
                break;
        }

        // Sadece durum değiştiğinde (Koşma->Zıplama veya Zıplama->Koşma) hesapla
        UpdateColliderShape();
    }

    private void UpdateColliderShape()
    {
        if (polyCol != null && spriteRenderer.sprite != null)
        {
            // Yöntem: Mevcut collider yollarını temizleyip yeniden oluşturmak.
            // Bu işlem ChangeState içinde olduğu için sadece zıplarken ve yere inerken tetiklenir.
            polyCol.pathCount = 0; 
        }
    }
}