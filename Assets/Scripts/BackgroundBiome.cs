using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "NewBiome", menuName = "Environment/Biome")]
public class BackgroundBiome : ScriptableObject
{
    public string biomeName;
    public List<LayerData> layers;
}
[System.Serializable]
public class LayerData
{
    public string layerName; // Inspector'da karışmaması için isim
    public Sprite sprite;
    public float scrollSpeedFactor;
    public int sortingOrder;
    public float scale = 1f; // <-- YENİ: Varsayılan değer 1 (Normal boy)
    public float yOffset = 0f; // <-- EKSTRA: Görseli yukarı/aşağı kaydırmak için
}