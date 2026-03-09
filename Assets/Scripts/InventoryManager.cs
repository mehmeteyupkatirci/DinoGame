using TMPro;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }

    [Header("Ekonomi")]
    public int currentGold = 0;
    public TextMeshProUGUI goldText;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        UpdateGoldUI();
    }

    // Altın eklendiğinde JokerManager'a haber verir
    public void AddGold(int amount)
    {
        currentGold += amount;
        UpdateGoldUI();
        
        // Cüzdana para girdiğinde JokerManager'daki Jokerleri tetikle
        if (JokerManager.Instance != null)
        {
            JokerManager.Instance.NotifyGoldCollected();
        }
    }

    // Dükkan için: Para harcama kontrolü
    public bool SpendGold(int amount)
    {
        if (currentGold >= amount)
        {
            currentGold -= amount;
            UpdateGoldUI();
            return true;
        }
        Debug.Log("Yetersiz altın!");
        return false;
    }

    private void UpdateGoldUI()
    {
        if (goldText != null) 
            goldText.text = "Gold: " + currentGold.ToString();
    }
}