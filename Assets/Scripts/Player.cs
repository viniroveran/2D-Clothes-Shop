using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class Player : MonoBehaviour
{
    // Reference to GameManager
    private GameManager _gameManager;
    // Reference to Inventory
    [SerializeField] private Inventory inventory;
    
    // Player equipped helmet
    public Item equippedHelmet;
    // Player equipped armor
    public Item equippedArmor;

    [SerializeField] private int initialGold;

    // Start is called before the first frame update
    private void Start()
    {
        _gameManager = GameManager.Instance;
        // Initialize inventory
        inventory.InitializeEmptyInventory();
        // Add initial gold
        inventory.AddGold(initialGold);
    }

    // Equip new item on Helmet Slot
    public void EquipHelmetSlot(Item item)
    {
        Debug.Log("Equipping (helmet) " + item.itemName);
        // Update displayed item on player
    }

    // Equip new item on Armor Slot
    public void EquipArmorSlot(Item item)
    {
        Debug.Log("Equipping (armor) " + item.itemName);
        // Update displayed item on player
    }

    // Buy an item
    public void BuyItem(Item item)
    {
        if (inventory.GetGold() > item.itemPrice)
        {
            inventory.RemoveGold(item.itemPrice);
            inventory.AddItem(inventory.emptySlots.First(), item.itemId);
            _gameManager.ShowPurchaseTooltip("Bought a new " + item.itemName + " for " + item.itemPrice + " gold coins");
            Debug.Log("Player bought " + item.itemName + " for " + item.itemPrice);
            return;
        }

        _gameManager.ShowPurchaseTooltip("Not enough gold!");
        Debug.Log("Player doesn't have enough gold to purchase item.");
    }
    
    // Sell an item
    public void SellItem(Item item)
    {
        int itemSlotIdInInventory = inventory.HasItem(item);
        
        // Return if the inventory doesn't have the item
        if (itemSlotIdInInventory == -1)
        {
            _gameManager.ShowPurchaseTooltip("You don't have this item!");
            Debug.Log("Player tried to sell an item not present in it's inventory.");
            return;
        }
        
        inventory.AddGold(item.itemPrice);
        inventory.RemoveItem(itemSlotIdInInventory);
        _gameManager.ShowPurchaseTooltip("Sold " + item.itemName + " for " + item.itemPrice + " gold coins");
        Debug.Log("Player sold "  + item.itemName + " for " + item.itemPrice);
    }
}
