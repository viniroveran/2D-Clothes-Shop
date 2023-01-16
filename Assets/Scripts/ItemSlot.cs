using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    private Inventory _inventory;
    private Player _player;
    
    // Slot info
    public int slotId;
    public bool isEmpty = false;
    
    // Slot item icon
    public Image slotItemIcon;
    
    // Item in slot
    public Item item;
    
    // Slot item button
    private Button _slotItemButton;

    private void Awake()
    {
        _player = GetComponentInParent<Player>();
        _slotItemButton = GetComponentInChildren<Button>();
    }

    public void PrepareButton(bool clearOnly)
    {
        // Clear any action already set to the button
        _slotItemButton.onClick.RemoveAllListeners();

        if (!clearOnly)
        {
            // Add onClick action according to item type
            switch (item.itemType)
            {
                case ItemType.Helmet:
                    // Equip new item
                    _slotItemButton.onClick.AddListener(delegate { _player.EquipHelmetSlot(item); });
                    break;
                
                case ItemType.Shoulders:
                    // Equip new item
                    _slotItemButton.onClick.AddListener(delegate { _player.EquipShouldersSlot(item); });
                    break;
                
                case ItemType.Armor:
                    // Equip new item
                    _slotItemButton.onClick.AddListener(delegate { _player.EquipArmorSlot(item); });
                    break;
            }
            // Add onClick action to remove item from that slot
            _slotItemButton.onClick.AddListener(delegate { _inventory.RemoveItem(slotId); });
        }
    }
}
