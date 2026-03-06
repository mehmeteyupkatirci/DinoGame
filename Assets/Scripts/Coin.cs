using UnityEngine;

public class Coin : MonoBehaviour
{
    [Header("Salınım Ayarları")]
    public float amplitude = 0.5f; // Ne kadar yukarı-aşağı gidecek
    public float frequency = 2f;   // Ne kadar hızlı sallanacak
    
    private float startY;

    private void OnEnable()
    {
        // Altın her aktif olduğunda bulunduğu Y pozisyonunu başlangıç noktası seçer
        startY = transform.position.y;
    }

    private void Update()
    {
        if (GameManager.Instance.isGameOver) return;

        // 1. Sola Hareket
        transform.Translate(Vector2.left * GameManager.Instance.gameSpeed * Time.deltaTime);

        // 2. Aşağı-Yukarı Salınım (Sinüs Dalgası)
        // Yeni Y pozisyonunu hesaplıyoruz
        float newY = startY + Mathf.Sin(Time.time * frequency) * amplitude;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);

        // Ekrandan çıkınca kapat
        if (transform.position.x < -15f)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            InventoryManager.Instance.AddGold(1);
            gameObject.SetActive(false);
        }
    }
}