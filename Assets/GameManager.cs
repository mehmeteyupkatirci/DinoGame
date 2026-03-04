using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Game Settings")]
    public float initialGameSpeed = 5f;
    public float speedMultiplier = 0.1f;
    public float maxGameSpeed = 15f;
    public float gameSpeed { get; private set; }
    public bool isGameOver { get; private set; }

    [Header("Day/Night Cycle")]
    public Camera mainCamera;
    public Color dayColor = Color.white;
    public Color nightColor = new Color(0.1f, 0.1f, 0.1f);
    public float cycleDuration = 30f; // Seconds per half cycle
    private float cycleTimer = 0f;

    [Header("UI Elements")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI restartText;

    private float score = 0f;

    private void Awake()
    {
        // Singleton pattern
        if (Instance == null) { Instance = this; }
        else { Destroy(gameObject); }
    }

    private void Start()
    {
        gameSpeed = initialGameSpeed;
        isGameOver = false;
        
        // Hide Game Over UI
        gameOverText.gameObject.SetActive(false);
        restartText.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (isGameOver)
        {
            // Handle restart
            if (Input.GetKeyDown(KeyCode.R) || Input.GetMouseButtonDown(0))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            return;
        }

        // Update Score
        score += Time.deltaTime * gameSpeed;
        scoreText.text = "Score: " + Mathf.FloorToInt(score).ToString("D5");

        // Increase Speed over time
        if (gameSpeed < maxGameSpeed)
        {
            gameSpeed += speedMultiplier * Time.deltaTime;
        }

        // Handle Day/Night Cycle
        cycleTimer += Time.deltaTime;
        float lerpFactor = Mathf.PingPong(cycleTimer / cycleDuration, 1f);
//        mainCamera.backgroundColor = Color.Lerp(dayColor, nightColor, lerpFactor);
    }

    public void GameOver()
    {
        isGameOver = true;
        gameSpeed = 0f; // Stop objects from moving
        
        gameOverText.gameObject.SetActive(true);
        restartText.gameObject.SetActive(true);
    }
}