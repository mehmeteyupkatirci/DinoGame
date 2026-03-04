using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Physics Settings")]
    public float jumpForce = 12f;
    public float gravityScale = 3f;
    
    private Rigidbody2D rb;
    private BoxCollider2D boxCol;
    private SpriteRenderer spriteRenderer;

    private bool isGrounded = true;

    // Simulated animation states
    public enum PlayerState { Idle, Run, Jump, Dead }
    public PlayerState currentState;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCol = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Set up snappy gravity for platforming
        rb.gravityScale = gravityScale;
        ChangeState(PlayerState.Run);
    }

    private void Update()
    {
        if (GameManager.Instance.isGameOver) return;

        // Input check (Space or Tap)
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) && isGrounded)
        {
            Jump();
        }
    }

    private void Jump()
    {
        // Reset vertical velocity to ensure consistent jump heights
        rb.velocity = new Vector2(rb.velocity.x, 0f);
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        isGrounded = false;
        ChangeState(PlayerState.Jump);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Ground detection
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
        // Obstacle collision
        if (collision.CompareTag("Obstacle") && !GameManager.Instance.isGameOver)
        {
            Die();
        }
    }

    private void Die()
    {
        ChangeState(PlayerState.Dead);
        GameManager.Instance.GameOver();
        // Stop the player's upward momentum if they die mid-air
        rb.velocity = Vector2.zero;
        rb.gravityScale = 0f;
    }

    // Since we don't have an Animator or assets, we simulate states with colors
    private void ChangeState(PlayerState newState)
    {
        currentState = newState;
        switch (currentState)
        {
            case PlayerState.Run:
                spriteRenderer.color = Color.green; // Running color
                break;
            case PlayerState.Jump:
                spriteRenderer.color = Color.yellow; // Jumping color
                break;
            case PlayerState.Dead:
                spriteRenderer.color = Color.red; // Dead color
                break;
        }
    }
}