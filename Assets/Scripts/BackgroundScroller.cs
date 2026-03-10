using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    public static BackgroundScroller Instance { get; private set; }

    [Header("Current Biome")]
    public BackgroundBiome currentBiome;
    public float textureWidth = 19.2f; // Görsellerinin genişliği

    private List<GameObject> activeLayers = new List<GameObject>();

    private void Awake()
    {
        Instance = this;
        InitializeBiome(currentBiome);
    }

    public void InitializeBiome(BackgroundBiome biome)
    {
        // Eski katmanları temizle (Biyom değişirken)
        foreach (GameObject obj in activeLayers) Destroy(obj);
        activeLayers.Clear();

        currentBiome = biome;

        foreach (var layerData in currentBiome.layers)
        {
            CreateLayer(layerData);
        }
    }

    private void CreateLayer(LayerData data)
    {
        // Katman için bir taşıyıcı obje oluştur
        GameObject layerContainer = new GameObject("Layer_" + data.sprite.name);
        layerContainer.transform.SetParent(this.transform);
        activeLayers.Add(layerContainer);

        // İki parça oluştur (Sonsuz döngü için)
        CreatePart(layerContainer.transform, data, 0);
        CreatePart(layerContainer.transform, data, textureWidth);

        // Hareket scriptini otomatik ekle
        var mover = layerContainer.AddComponent<LayerMover>();
        mover.speedFactor = data.scrollSpeedFactor;
        mover.width = textureWidth;
    }

   private void CreatePart(Transform parent, LayerData data, float startX)
{
    GameObject part = new GameObject(data.layerName + "_Part");
    part.transform.SetParent(parent);
    
    // Scale ve Pozisyon ayarını uygula
    part.transform.localScale = new Vector3(data.scale, data.scale, 1);
    part.transform.localPosition = new Vector3(startX, data.yOffset, 0);

    var renderer = part.AddComponent<SpriteRenderer>();
    renderer.sprite = data.sprite;
    renderer.sortingOrder = data.sortingOrder;
}
}