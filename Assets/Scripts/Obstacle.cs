using UnityEngine;

public class Obstacle : MonoBehaviour
{
    // Obje bu X değerinin soluna geçtiğinde yok sayılacak
    private float leftBoundary = -15f; 

    private void Update()
    {
        if (GameManager.Instance.isGameOver) return;

        // Sola hareket
        transform.Translate(Vector2.left * GameManager.Instance.gameSpeed * Time.deltaTime);

        // Ekranın dışına çıktı mı kontrolü
        if (transform.position.x < leftBoundary)
        {
            // Eğer Object Pooling (Havuzlama) kullanıyorsan:
            gameObject.SetActive(false); 
            
            // Eğer pooling kullanmıyorsan direkt yok etmek için:
            // Destroy(gameObject);
        }
    }
}