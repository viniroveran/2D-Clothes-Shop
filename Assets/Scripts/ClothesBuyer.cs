using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClothesBuyer : MonoBehaviour
{
    // Reference to Player
    public Player player;
    
    // Start is called before the first frame update
    void Start()
    {
        foreach (ShopSlot shopSlot in GetComponentsInChildren<ShopSlot>())
        {
            shopSlot.PrepareSellButton();
            shopSlot.player = player;
        }
    }
}
