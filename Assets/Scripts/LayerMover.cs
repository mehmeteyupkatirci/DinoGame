using UnityEngine;

public class LayerMover : MonoBehaviour
{
    public float speedFactor;
    public float width;
    private Transform part1, part2;

    private void Start()
    {
        part1 = transform.GetChild(0);
        part2 = transform.GetChild(1);
    }

    private void Update()
    {
        if (GameManager.Instance.isGameOver) return;

        float speed = GameManager.Instance.gameSpeed * speedFactor;
        transform.position += Vector3.left * speed * Time.deltaTime;

        // Döngü kontrolü (Global pozisyona göre)
        if (part1.position.x <= -width)
            part1.position = new Vector3(part2.position.x + width, part1.position.y, 0);

        if (part2.position.x <= -width)
            part2.position = new Vector3(part1.position.x + width, part2.position.y, 0);
    }
}