using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    public Transform bgPart1;
    public Transform bgPart2;
    public float width = 18f; 

    [Range(0f, 1f)]
    [Tooltip("0.5 yaparsan arka plan oyun hızının yarısı kadar hızlanır. 1 yaparsan yerle aynı hızda gider.")]
    public float scrollSpeedFactor = 0.5f; 

    private void Update()
    {
        // Oyun bittiyse durdur
        if (GameManager.Instance.isGameOver) return;
        float currentScrollSpeed = GameManager.Instance.gameSpeed * scrollSpeedFactor;

        // Hareket ettir
        bgPart1.Translate(Vector2.left * currentScrollSpeed * Time.deltaTime);
        bgPart2.Translate(Vector2.left * currentScrollSpeed * Time.deltaTime);

        // Sonsuz döngü kontrolü
        if (bgPart1.position.x < -width)
        {
            bgPart1.position = new Vector2(bgPart2.position.x + width, bgPart1.position.y);
        }
        
        if (bgPart2.position.x < -width)
        {
            bgPart2.position = new Vector2(bgPart1.position.x + width, bgPart2.position.y);
        }
    }
}