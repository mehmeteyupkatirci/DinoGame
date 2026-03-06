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
    private BoxCollider2D boxCol; 

    private bool isGrounded = true;
    private int currentRunFrame;
    private float animationTimer;

    public enum PlayerState { Run, Jump, Dead }
    public PlayerState currentState;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCol = GetComponent<BoxCollider2D>();

        rb.gravityScale = gravityScale;
        
        // İlk sprite'ı ata ama collider'a DOKUNMA (elle ayarladığımız kalsın)
        if (runSprites.Length > 0) spriteRenderer.sprite = runSprites[0];
        
        ChangeState(PlayerState.Run);
    }

    private void Update()
    {
        if (GameManager.Instance.isGameOver) return;

        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) && isGrounded)
        {
            Jump();
        }

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
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, 0f);
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        isGrounded = false;
        ChangeState(PlayerState.Jump);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            if (!GameManager.Instance.isGameOver)
            {
                ChangeState(PlayerState.Run);
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
        // Burada UpdateColliderSize() gibi bir fonksiyon çağırmıyoruz! 
        // Collider'ı bir kez ayarlayacağız ve hep öyle kalacak.
    }
}