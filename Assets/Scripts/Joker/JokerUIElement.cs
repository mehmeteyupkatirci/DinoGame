using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class JokerUIElement : MonoBehaviour
{
    [Header("UI References")]
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI descText;
    public Image iconImage;

    private JokerData currentData;

    public void Setup(JokerData data)
    {
        currentData = data;
        titleText.text = data.jokerName;
        descText.text = data.description;
        if (data.jokerIcon != null) iconImage.sprite = data.jokerIcon;

        // Butonun tıklama olayını temizle ve yeni veriyi ata
        Button btn = GetComponent<Button>();
        btn.onClick.RemoveAllListeners();
        btn.onClick.AddListener(SelectThisJoker);
    }

    private void SelectThisJoker()
    {
        // JokerPickerUI üzerinden seçimi onayla
        JokerPickerUI.Instance.OnJokerSelected(currentData);
    }
}