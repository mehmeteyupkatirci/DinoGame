using System.Collections.Generic;
using UnityEngine;

public class JokerDatabase : MonoBehaviour
{
    public static JokerDatabase Instance { get; private set; }
    
    // Inspector'dan oluşturduğumuz JokerData (ScriptableObject) dosyalarını buraya atacağız
    public List<JokerData> allAvailableJokers;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public List<JokerData> GetRandomJokers(int count)
    {
        List<JokerData> randomList = new List<JokerData>();
        List<JokerData> tempPool = new List<JokerData>(allAvailableJokers);

        for (int i = 0; i < count; i++)
        {
            if (tempPool.Count == 0) break;
            int index = Random.Range(0, tempPool.Count);
            randomList.Add(tempPool[index]);
            tempPool.RemoveAt(index); // Aynı Jokerin iki kez çıkmasını engelle
        }
        return randomList;
    }
}