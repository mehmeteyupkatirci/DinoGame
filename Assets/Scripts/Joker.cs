using UnityEngine;

[System.Serializable]
public abstract class Joker
{
    public string jokerName;
    public string description;
    public int price;

    // Joker takıldığında bir kez çalışır (Örn: Hızı +5 artır)
    public virtual void OnEquip() { }

    // Joker çıkartıldığında bir kez çalışır (Örn: Hızı -5 azalt, eski haline getir)
    public virtual void OnUnequip() { }

    // Her altın toplandığında çalışır
    public virtual void OnGoldCollected() { }

    // Her karede skor hesaplanırken araya girer (Matematiği değiştiren yer!)
    public virtual float ModifyScore(float currentBaseScore) 
    { 
        return currentBaseScore; // Varsayılan: Skoru değiştirme
    }
}