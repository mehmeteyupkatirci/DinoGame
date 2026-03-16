using UnityEngine;
using System.Collections.Generic;

public class JokerPickerUI : MonoBehaviour
{
    public static JokerPickerUI Instance { get; private set; }

    [Header("Panel & Cards")]
    public GameObject pickerPanel; 
    public List<JokerUIElement> cardUIElements; // 3 kartın script referansları

   private void Awake()
{
    // Singleton atamasını en başta ve kontrol ederek yap
    if (Instance == null) 
    {
        Instance = this;
        // Opsiyonel: Sahneler arası geçişte yok olmasın dersen
        // transform.SetParent(null); 
        // DontDestroyOnLoad(gameObject);
    }
    else 
    {
        Destroy(gameObject);
        return;
    }

    // Panel başlangıçta kapalı olmalı, ana GameObject değil!
    if (pickerPanel != null) pickerPanel.SetActive(false);
}

    public void ShowPicker()
    {
        pickerPanel.SetActive(true);
        
        // Database'den rastgele 3 joker al
        List<JokerData> randomJokers = JokerDatabase.Instance.GetRandomJokers(3);

        for (int i = 0; i < cardUIElements.Count; i++)
        {
            if (i < randomJokers.Count)
            {
                cardUIElements[i].gameObject.SetActive(true);
                cardUIElements[i].Setup(randomJokers[i]);
            }
            else
            {
                cardUIElements[i].gameObject.SetActive(false);
            }
        }
    }

    public void OnJokerSelected(JokerData selectedData)
    {
        // Reflection ile Joker class'ını oluştur
        System.Type type = System.Type.GetType(selectedData.jokerClassName);
        if (type != null)
        {
            Joker newJoker = (Joker)System.Activator.CreateInstance(type);
            JokerManager.Instance.EquipJoker(newJoker);
        }

        // Oyuna geri dön
        pickerPanel.SetActive(false);
        Time.timeScale = 1f; 
    }
}