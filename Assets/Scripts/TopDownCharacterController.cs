using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownCharacterController : MonoBehaviour
{
    [SerializeField] private float speed = 3; // character movement speed
    private Rigidbody2D _characterRigidBody2D;
    private GameManager _gameManager;

    private void Start()
    {
        _characterRigidBody2D = GetComponent<Rigidbody2D>();
        _gameManager = GameManager.Instance;
    }

    private void Update()
    {
        Vector2 dir = Vector2.zero;

        // Move Up
        if (Input.GetKey(KeyCode.W))
            dir.y = 1;
        
        // Move Left
        if (Input.GetKey(KeyCode.A))
            dir.x = -1;

        // Move Down
        if (Input.GetKey(KeyCode.S))
            dir.y = -1;
        
        // Move Right
        if (Input.GetKey(KeyCode.D))
            dir.x = 1;

        // Normalize dir vector
        dir.Normalize();
        // Move character
        _characterRigidBody2D.velocity = speed * dir;
        
        // Interact with NPC
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (_gameManager.isInInteractionRange)
            {
                // If the NPC is a buyer, open the inventory to sell our items
                if (_gameManager.canInteractWithBuyer)
                    _gameManager.ToggleInventory(true);

                // If the NPC is a seller, open the Clothes Shop
                if (_gameManager.canInteractWithSeller)
                    _gameManager.ToggleClothesShop(true);
            }
        }

        // Open/close Inventory UI
        if (Input.GetKeyDown(KeyCode.I))
            _gameManager.ToggleInventory(!_gameManager.isInventoryOpen);
        
        // Open/close Pause Menu UI
        if (Input.GetKeyDown(KeyCode.Escape))
            _gameManager.Pause(!_gameManager.isPaused);

        // Make sure the cursor is always showing if the Inventory or Clothes Shop UI are open
        if (_gameManager.isInventoryOpen || _gameManager.isClothesShopOpen)
        {
            _gameManager.ToggleCursor(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("BuyerNPC"))
        {
            _gameManager.ToggleInteractionTooltip(true, false, true);
            Debug.Log("Found a Buyer NPC!");
        }

        if (other.CompareTag("SellerNPC"))
        {
            _gameManager.ToggleInteractionTooltip(true, true, false);
            Debug.Log("Found a Seller NPC!");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("BuyerNPC") || other.CompareTag("SellerNPC"))
        {
            _gameManager.ToggleInteractionTooltip(false, false, false);
            
            if (_gameManager.isClothesShopOpen)
                _gameManager.ToggleClothesShop(false);
            
            if (_gameManager.isInventoryOpen)
                _gameManager.ToggleInventory(false);
            
            Debug.Log("Moved away from NPC!");
        }
    }
}
