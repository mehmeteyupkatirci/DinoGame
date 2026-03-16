using UnityEngine;
using System.Collections.Generic;

public class MilestoneManager : MonoBehaviour
{
    public List<float> milestones = new List<float> { 55f, 300f, 750f, 1500f };
    private int currentMilestoneIndex = 0;

    private void Update()
    {
        if (GameManager.Instance.isGameOver) return;

        if (currentMilestoneIndex < milestones.Count && 
            GameManager.Instance.distance >= milestones[currentMilestoneIndex])
        {
            currentMilestoneIndex++;
            TriggerJokerSelection();
        }
    }

    void TriggerJokerSelection()
    {
        // Oyunu durdur (Pause)
        Time.timeScale = 0f; 
        
        // UI'yı aç ve rastgele 3 joker getir
        JokerPickerUI.Instance.ShowPicker();
    }
}