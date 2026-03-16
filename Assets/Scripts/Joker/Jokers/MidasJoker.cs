using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MidasJoker : Joker
{
    public override void OnGoldCollected() {
        InventoryManager.Instance.AddGold(10, false); // Her altına +10 bonus
    }
}
