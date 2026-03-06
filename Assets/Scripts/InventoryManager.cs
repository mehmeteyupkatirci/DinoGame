using TMPro;
using UnityEngine;
using UnityEngine.UI;

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

    public void AddGold(int amount)
    {
        currentGold += amount;
        UpdateGoldUI();
        // Buraya isterseniz bir "Para kazanma sesi" ekleyebiliriz
    }

    public bool SpendGold(int amount)
    {
        if (currentGold >= amount)
        {
            currentGold -= amount;
            UpdateGoldUI();
            return true;
        }
        return false; // Yetersiz bakiye
    }

    private void UpdateGoldUI()
    {
        if (goldText != null)
        {
            goldText.text = "$" + currentGold.ToString();
        }
    }
}