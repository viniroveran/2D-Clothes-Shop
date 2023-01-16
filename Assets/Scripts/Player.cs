using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Player : MonoBehaviour
{
    [SerializeField] private Inventory inventory;
    
    // Helmet Slot variables
    public Item equippedHelmet;
    // Shoulder Slot variables
    public Item equippedShoulders;
    // Armor Slot variables
    public Item equippedArmor;

    [SerializeField] private int initialGold;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize inventory
        inventory.InitializeEmptyInventory();
        // Add initial gold
        inventory.AddGold(initialGold);
    }
    
    // Equip new item on Helmet Slot
    public void EquipHelmetSlot(Item newItem)
    {
        // Update displayed item on player
    }
    
    // Equip new item on Shoulders Slot
    public void EquipShouldersSlot(Item newItem)
    {
        // Update displayed item on player
    }
    
    // Equip new item on Armor Slot
    public void EquipArmorSlot(Item newItem)
    {
        // Update displayed item on player
    }
}
