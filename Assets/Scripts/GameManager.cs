using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("UI References")]
    public TextMeshProUGUI speedText;
    public TextMeshProUGUI distanceText;
    public TextMeshProUGUI goldText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI restartText;

    [Header("Core Stats")]
    public float gameSpeed;
    public float initialGameSpeed = 5f;
    public float speedMultiplier = 0.1f;
    public float maxGameSpeed = 25f;

    public float distance = 0f;
    public float score = 0f;
    // Not: Altın verisini InventoryManager'dan çekeceğiz ancak formülde kullanacağız.

    [Header("Balatro Logic")]
    public float currentMult = 1.0f;
    public bool isGameOver { get; private set; }

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        gameSpeed = initialGameSpeed;
        isGameOver = false;

        gameOverText.gameObject.SetActive(false);
        restartText.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (isGameOver)
        {
            if (Input.GetKeyDown(KeyCode.R) || Input.GetMouseButtonDown(0))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            return;
        }

        HandleMovementAndStats();
        CalculateFinalScore();
        UpdateUI();
    }

    private void HandleMovementAndStats()
    {
        // Zamanla hız artışı
        if (gameSpeed < maxGameSpeed)
        {
            gameSpeed += speedMultiplier * Time.deltaTime;
        }

        // Mesafe artışı (Hız * Zaman)
        distance += gameSpeed * Time.deltaTime;
    }
    private void CalculateFinalScore()
    {
        int currentGold = InventoryManager.Instance.currentGold;

        // Temel hesaplama: Hız + Mesafe + Altın
        float baseCalculation = gameSpeed + distance + currentGold;

        // JOKERLER DEVREYE GİRİYOR:
        // JokerManager'a gidip "bu skoru al ve Jokerlerin üzerinden geçir" diyoruz.
        float scoreAfterJokers = JokerManager.Instance.ApplyJokerModifications(baseCalculation);

        // En son Balatro Mult'u ekle
        score = scoreAfterJokers * currentMult;
    }

    private void UpdateUI()
    {
        speedText.text = "Hız: " + gameSpeed.ToString("F1");
        distanceText.text = "Mesafe: " + Mathf.FloorToInt(distance).ToString() + "m";
        goldText.text = "Gold: " + InventoryManager.Instance.currentGold.ToString();
        scoreText.text = "Score: " + Mathf.FloorToInt(score).ToString();
    }

    public void AddMult(float amount)
    {
        currentMult += amount;
    }

    // Jokerler aracılığıyla hızı doğrudan etkilemek için
    public void ModifySpeed(float amount)
    {
        gameSpeed = Mathf.Clamp(gameSpeed + amount, 0, maxGameSpeed + 20);
    }

    public void GameOver()
    {
        isGameOver = true;
        gameSpeed = 0f;
        gameOverText.gameObject.SetActive(true);
        restartText.gameObject.SetActive(true);
    }
}