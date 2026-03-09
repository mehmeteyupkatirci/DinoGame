using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldenJoker : Joker
{
    public GoldenJoker()
    {
        jokerName = "Altın Joker";
        description = "Toplam skoru, sahip olduğun altın sayısıyla çarpar!";
        price = 20;
    }

    public override float ModifyScore(float currentBaseScore)
    {
        int gold = InventoryManager.Instance.currentGold;
        
        // Eğer hiç altın yoksa 1 ile çarp (skor sıfırlanmasın)
        float multiplier = gold > 0 ? gold : 1;
        
        return currentBaseScore * multiplier;
    }
}