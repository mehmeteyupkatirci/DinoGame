using UnityEngine;

[CreateAssetMenu(fileName = "NewJoker", menuName = "JokerData")]
public class JokerData : ScriptableObject
{
    public string jokerName;
    [TextArea] public string description;
    public Sprite jokerIcon;
    public int unlockPrice; // İlk kez dükkandan almak için gereken fiyat
    public bool isUnlocked = false; // Oyuncu bunu sandıktan buldu mu?
    
    // Hangi Joker tipinde olduğunu belirlemek için bir enum veya string
    public string jokerClassName; 
}