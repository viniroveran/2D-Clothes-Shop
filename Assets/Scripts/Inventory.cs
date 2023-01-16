using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    // Gold UI
    [SerializeField] private TextMeshProUGUI goldUI;
    
    // List of item slots
    public List<ItemSlot> itemSlots;
    
    // List of empty slots
    [HideInInspector] public List<int> emptySlots = new List<int>();

    // Player gold
    private int _gold;
    
    // Reference to GameManager
    private GameManager _gameManager;
    
    // Creates an empty item slot
    private static void CreateEmptyItemSlot(ItemSlot itemSlot)
    {
        if (itemSlot == null)
            Debug.LogError("Item slot " + itemSlot.slotId + " not configured!");
        else
        {
            itemSlot.gameObject.SetActive(true);
            itemSlot.item = null;
            itemSlot.slotItemIcon.enabled = false;
            itemSlot.isEmpty = true;
            // Remove item slot button action
            itemSlot.PrepareButton(true);
        }
    }

    // Initializes an empty inventory
    public void InitializeEmptyInventory()
    {
        // Initialize the inventory with empty items
        foreach (ItemSlot slot in itemSlots)
            CreateEmptyItemSlot(slot);

        UpdateAvailableSlots();
    }
    
    // Updates the list of empty slotIds
    private void UpdateAvailableSlots()
    {
        foreach (ItemSlot slot in itemSlots)
        {
            if (slot == null)
                Debug.LogError("Item slot " + slot.slotId + " not configured!");
            else
            {
                // Remove slotId from list of empty slots
                emptySlots.Remove(slot.slotId);

                // Add slotId to list of empty slots
                if (slot.isEmpty)
                    emptySlots.Add(slot.slotId);
            }
        }
        
        // Sort the list so that we can always pick the first available slot to put an item in
        emptySlots.Sort();
    }

    // Get gold in inventory
    public int GetGold()
    {
        return _gold;
    }
    
    // Add gold to inventory
    public void AddGold(int amount)
    {
        Debug.Log("Gold before: " + _gold);
        _gold += amount;
        Debug.Log("Added " + amount + " gold. Total: " + _gold);
    }
    
    // Remove gold from inventory
    public void RemoveGold(int amount)
    {
        Debug.Log("Gold before: " + _gold);
        _gold -= amount;
        Debug.Log("Removed " + amount + " gold. Total: " + _gold);
    }
    // Adds an item to the given slotIndex
    public void AddItem(int slotIndex, int itemID)
    {
        if (itemSlots[slotIndex].isEmpty)
        {
            // Set which item is inside that slot
            itemSlots[slotIndex].item = GetItemFromID(itemID);

            // Set the slot to NOT empty
            itemSlots[slotIndex].isEmpty = false;
            
            // Set item icon
            itemSlots[slotIndex].slotItemIcon.enabled = true;
            itemSlots[slotIndex].slotItemIcon.sprite = itemSlots[slotIndex].item.itemIcon;
            
            // Add item slot button action
            itemSlots[slotIndex].PrepareButton(false);

            // Update the empty slots
            UpdateAvailableSlots();

            Debug.Log("Added an item to the slot " + slotIndex);
        }
        else
            Debug.LogError("Slot " + slotIndex + " not empty.");
    }

    // Gets Item object from given slotIndex 
    public Item GetItemFromSlot(int slotIndex)
    {
        return itemSlots[slotIndex].item == null ? null : itemSlots[slotIndex].item;
    }
    
    // Gets itemID from given slotIndex
    public int GetItemIDFromSlot(int slotIndex)
    {
        if (itemSlots[slotIndex].item == null)
            return 0;

        return itemSlots[slotIndex].item.itemId;
    }

    // Gets itemID from dictionary of items
    private Item GetItemFromID(int itemID)
    {
        return itemID == 0 ? null : _gameManager.ItemsDictionary[itemID];
    }
    
    // Remove item from inventory
    public void RemoveItem(int slotIndex)
    {
        // item could not be found
        if (slotIndex == -1)
            return;

        // Create an empty slot where the item was
        CreateEmptyItemSlot(itemSlots[slotIndex]);

        // Update the empty slots
        UpdateAvailableSlots();
        
        Debug.Log("Removed: " + itemSlots[slotIndex].item.itemName + " | Slot: " + slotIndex);
    }

    // Start is called before the first frame update
    private void Start()
    {
        _gameManager = GameManager.Instance;
    }

    // Update is called once per frame
    private void Update()
    {
        goldUI.text = "Gold: " + GetGold();
    }
}
