using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private void Update()
    {
        if (GameManager.Instance.isGameOver) return;

        // Move the obstacle left
        transform.Translate(Vector2.left * GameManager.Instance.gameSpeed * Time.deltaTime);
    }

    private void OnBecameInvisible()
    {
        // When the obstacle goes off the camera view, deactivate it (Object Pooling return)
        gameObject.SetActive(false);
    }
}