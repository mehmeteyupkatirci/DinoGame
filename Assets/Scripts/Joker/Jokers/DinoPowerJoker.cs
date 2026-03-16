public class DinoPowerJoker : Joker
{
    public override void OnEquip() {
        GameManager.Instance.ModifySpeed(5f); // Hızı kalıcı artır
    }
    public override void OnUnequip() {
        GameManager.Instance.ModifySpeed(-5f); // Çıkartılırsa hızı geri al
    }
}