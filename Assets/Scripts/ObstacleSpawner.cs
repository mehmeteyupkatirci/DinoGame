using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    public GameObject cactusPrefab;
    public GameObject birdPrefab;
    public GameObject coinPrefab;
    
    public float minSpawnTime = 1f;
    public float maxSpawnTime = 2.5f;
    
    [Header("Coin Settings")]
    [Range(0, 1)] public float coinSpawnChance = 0.5f; // Her engel dalgasında altın çıkma şansı
    public float minCoinY = -1.0f; // Dinozorun yerdeyken kafasıyla çarpabileceği alt sınır
    public float maxCoinY = 2.0f;  // Dinozorun zıplayınca ulaşabileceği üst sınır

    private float spawnTimer;

    [Header("Pooling")]
    public int poolSize = 10;
    private List<GameObject> cactusPool;
    private List<GameObject> birdPool;
    private List<GameObject> coinPool; // Altın havuzu eklendi

    private void Start()
    {
        InitializePools();
        SetRandomSpawnTimer();
    }

    private void Update()
    {
        if (GameManager.Instance.isGameOver) return;

        spawnTimer -= Time.deltaTime;

        if (spawnTimer <= 0f)
        {
            SpawnObstacle();
            
            // Her engel çıktığında şansa bağlı altın çıkar
            if (Random.value < coinSpawnChance)
            {
                SpawnCoin();
            }

            SetRandomSpawnTimer();
        }
    }

    private void InitializePools()
    {
        cactusPool = new List<GameObject>();
        birdPool = new List<GameObject>();
        coinPool = new List<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            CreatePooledObject(cactusPrefab, cactusPool);
            CreatePooledObject(birdPrefab, birdPool);
            CreatePooledObject(coinPrefab, coinPool);
        }
    }

    private void CreatePooledObject(GameObject prefab, List<GameObject> pool)
    {
        GameObject obj = Instantiate(prefab);
        obj.SetActive(false);
        pool.Add(obj);
    }

    private void SpawnCoin()
    {
        GameObject coin = GetPooledObject(coinPool);
        if (coin != null)
        {
            // Yükseklik ayarı: Yer seviyesine göre ayarla
            // Dinozorun zıplama gücüne göre buradaki Y değerlerini test ederek daraltabilirsin
            float randomY = Random.Range(minCoinY, maxCoinY); 
            coin.transform.position = new Vector3(transform.position.x + 2f, randomY, 0); // Engelin biraz arkasında çıksın
            coin.SetActive(true);
        }
    }

    private void SpawnObstacle()
    {
        bool spawnBird = Random.value > 0.7f;
        List<GameObject> activePool = spawnBird ? birdPool : cactusPool;

        GameObject obstacle = GetPooledObject(activePool);
        
        if (obstacle != null)
        {
            Vector3 spawnPos = transform.position;
            if (spawnBird) spawnPos.y += 1.5f; 

            obstacle.transform.position = spawnPos;
            obstacle.SetActive(true);
        }
    }

    private GameObject GetPooledObject(List<GameObject> pool)
    {
        foreach (GameObject obj in pool)
        {
            if (!obj.activeInHierarchy) return obj;
        }
        return null; 
    }

    private void SetRandomSpawnTimer()
    {
        float speedRatio = GameManager.Instance.initialGameSpeed / GameManager.Instance.gameSpeed;
        spawnTimer = Random.Range(minSpawnTime, maxSpawnTime) * speedRatio;
    }
}