using UnityEngine;

public class SpeedFreakJoker : Joker
{
    public SpeedFreakJoker()
    {
        jokerName = "Hız Tutkunu";
        description = "Oyun hızı 15'i geçtiğinde toplam skoru x5 yapar. Hız felakettir, puan ise berekettir!";
        price = 40;
    }

    public override float ModifyScore(float currentBaseScore)
    {
        // GameManager'daki anlık hıza bakıyoruz
        float currentSpeed = GameManager.Instance.gameSpeed;

        // ŞART: Hız 15'ten büyük mü?
        if (currentSpeed >= 6f)
        {
            return currentBaseScore * 5f; // Şart sağlanırsa 5 katı puan
        }

        return currentBaseScore; // Sağlanmazsa etkisiz eleman
    }
}