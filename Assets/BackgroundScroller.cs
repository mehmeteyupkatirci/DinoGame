using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    public Transform bgPart1;
    public Transform bgPart2;
    public float width = 18f; // The horizontal size of your background piece

    private void Update()
    {
        if (GameManager.Instance.isGameOver) return;

        // Move background left (slower than obstacles for parallax effect)
        float scrollSpeed = GameManager.Instance.gameSpeed * 0.5f;

        bgPart1.Translate(Vector2.left * scrollSpeed * Time.deltaTime);
        bgPart2.Translate(Vector2.left * scrollSpeed * Time.deltaTime);

        // If part 1 goes too far left, teleport it to the right of part 2
        if (bgPart1.position.x < -width)
        {
            bgPart1.position = new Vector2(bgPart2.position.x + width, bgPart1.position.y);
        }
        
        // If part 2 goes too far left, teleport it to the right of part 1
        if (bgPart2.position.x < -width)
        {
            bgPart2.position = new Vector2(bgPart1.position.x + width, bgPart2.position.y);
        }
    }
}