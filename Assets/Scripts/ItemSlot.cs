using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    [HideInInspector] public Player player;
    
    // Slot info
    public int slotId;
    public bool isEmpty = false;
    public Image slotIcon;
    
    // Item in slot
    public Item item;
    
    // Slot item button
    private Button _slotItemButton;
    
    // Slot item price
    public TextMeshProUGUI itemNameText;

    private void Awake()
    {
        itemNameText.text = "";
        player = GetComponentInParent<Player>();
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
                    // Equip new item on click
                    _slotItemButton.onClick.AddListener(delegate { player.EquipHelmetSlot(item); });
                    break;
                case ItemType.Armor:
                    // Equip new item on click
                    _slotItemButton.onClick.AddListener(delegate { player.EquipArmorSlot(item); });
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
