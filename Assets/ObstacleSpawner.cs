using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    public GameObject cactusPrefab;
    public GameObject birdPrefab;
    
    public float minSpawnTime = 1f;
    public float maxSpawnTime = 2.5f;
    
    private float spawnTimer;

    [Header("Pooling")]
    public int poolSize = 10;
    private List<GameObject> cactusPool;
    private List<GameObject> birdPool;

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
            SetRandomSpawnTimer();
        }
    }

    private void InitializePools()
    {
        cactusPool = new List<GameObject>();
        birdPool = new List<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            GameObject cactus = Instantiate(cactusPrefab, transform.position, Quaternion.identity);
            cactus.SetActive(false);
            cactusPool.Add(cactus);

            GameObject bird = Instantiate(birdPrefab, transform.position + Vector3.up * 1.5f, Quaternion.identity);
            bird.SetActive(false);
            birdPool.Add(bird);
        }
    }

    private void SpawnObstacle()
    {
        // 70% chance for Cactus, 30% chance for Bird
        bool spawnBird = Random.value > 0.7f;
        List<GameObject> activePool = spawnBird ? birdPool : cactusPool;

        GameObject obstacle = GetPooledObject(activePool);
        
        if (obstacle != null)
        {
            // Reset position to spawner position
            Vector3 spawnPos = transform.position;
            if (spawnBird) spawnPos.y += 1.5f; // Elevate birds

            obstacle.transform.position = spawnPos;
            obstacle.SetActive(true);
        }
    }

    private GameObject GetPooledObject(List<GameObject> pool)
    {
        // Find the first inactive object in the pool
        foreach (GameObject obj in pool)
        {
            if (!obj.activeInHierarchy)
            {
                return obj;
            }
        }
        return null; // Pool is full (unlikely with fast despawn, but safe)
    }

    private void SetRandomSpawnTimer()
    {
        // Decrease spawn time slightly as the game speeds up for consistent density
        float speedRatio = GameManager.Instance.initialGameSpeed / GameManager.Instance.gameSpeed;
        spawnTimer = Random.Range(minSpawnTime, maxSpawnTime) * speedRatio;
    }
}