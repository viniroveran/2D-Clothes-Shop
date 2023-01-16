using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Helmet,
    Shoulders,
    Armor
}

public class Item : MonoBehaviour
{
    // Items Variables
    public int itemId;
    public ItemType itemType;
    public string itemName;
    public Sprite itemIcon;
}
