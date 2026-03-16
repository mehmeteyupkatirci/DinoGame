using System.Collections.Generic;
using UnityEngine;

public class JokerManager : MonoBehaviour
{
    public static JokerManager Instance { get; private set; }

    [Header("Active Jokers")]
    public List<Joker> activeJokers = new List<Joker>();
    public int maxSlots = 5;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        // TEST: Oyuna Golden Joker eklenmiş gibi başla
        //  EquipJoker(new GoldenJoker());
        //  EquipJoker(new RandomJoker());
        //  EquipJoker(new SpeedFreakJoker());
    }

    public void EquipJoker(Joker newJoker)
    {
        if (activeJokers.Count < maxSlots)
        {
            activeJokers.Add(newJoker);
            newJoker.OnEquip();
            Debug.Log(newJoker.jokerName + " kuşanıldı!");
        }
    }

    public void NotifyGoldCollected()
    {
        foreach (Joker joker in activeJokers)
        {
            joker.OnGoldCollected();
        }
    }

    public void UnequipJoker(Joker targetJoker)
    {
        if (activeJokers.Contains(targetJoker))
        {
            targetJoker.OnUnequip();
            activeJokers.Remove(targetJoker);
        }
    }

    // GameManager'ın skor hesaplarken çağıracağı fonksiyon
    public float ApplyJokerModifications(float baseScore)
    {
        float modifiedScore = baseScore;
        foreach (Joker joker in activeJokers)
        {
            modifiedScore = joker.ModifyScore(modifiedScore);
        }
        return modifiedScore;
    }
}