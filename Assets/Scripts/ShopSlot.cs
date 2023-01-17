using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ShopSlot : MonoBehaviour
{
    [HideInInspector] public Player player;
    
    // Slot info
    public Image slotIcon;
    
    // Item in slot
    public Item item;
    
    // Slot item button
    private  Button _slotItemButton;
    
    // Slot item price
    private TextMeshProUGUI _itemPriceText;

    private void Start()
    {
        _itemPriceText = GetComponentInChildren<TextMeshProUGUI>();
        _itemPriceText.text = item.itemPrice + " gold";
        slotIcon.sprite = item.itemIcon;
    }

    public void PrepareBuyButton()
    {
        _slotItemButton = GetComponentInChildren<Button>();
        // Clear any action already set to the button
        _slotItemButton.onClick.RemoveAllListeners();
        
        // Equip new item on click
        _slotItemButton.onClick.AddListener(delegate { player.BuyItem(item); });
    }
    
    public void PrepareSellButton()
    {
        _slotItemButton = GetComponentInChildren<Button>();
        // Clear any action already set to the button
        _slotItemButton.onClick.RemoveAllListeners();
        
        // Equip new item on click
        _slotItemButton.onClick.AddListener(delegate { player.SellItem(item); });
    }
}
