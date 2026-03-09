using UnityEngine;

public class RandomJoker : Joker
{
    public RandomJoker()
    {
        jokerName = "Katalizör Joker";
        description = "Toplama işlemini unut! Skoru: (Hız * Mesafe * Altın) olarak hesaplar.";
        price = 50;
    }

    public override float ModifyScore(float currentBaseScore)
    {
        // GameManager'daki ham verilere ulaşıyoruz
        float speed = GameManager.Instance.gameSpeed;
        float dist = GameManager.Instance.distance;
        int gold = InventoryManager.Instance.currentGold;

        // Çarpanların 0 olup skoru yutmasını engellemek için en az 1 alıyoruz
        float safeGold = gold > 0 ? gold : 1;
        float safeDist = dist > 0 ? dist : 1;

        // Diyagramındaki o meşhur formül:
        float chaoticScore = speed * safeDist * safeGold;

        return chaoticScore;
    }
}