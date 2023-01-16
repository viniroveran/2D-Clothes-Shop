using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null; // Reference to the singleton
    private AsyncOperation _asyncOperation;
    [HideInInspector] public bool isPaused; // Is the game paused?
    [HideInInspector] public bool isInInteractionRange = false; // Is the player in interaction range?
    [HideInInspector] public bool canInteractWithSeller = false; // Is the player in interaction range with a seller?
    [HideInInspector] public bool canInteractWithBuyer = false; // Is the player in interaction range with a buyer?
    [HideInInspector] public bool isClothesShopOpen = false; // Is the clothes shop UI open?
    [HideInInspector] public bool isClothesBuyerOpen = false; // Is the clothes buyer UI open?
    [HideInInspector] public bool isInventoryOpen = false; // Is the inventory UI open?
    
    [Header("Items")]
    public List<Item> itemPrefabs = new List<Item>(); // List of item prefabs
    public Dictionary<int, Item> ItemsDictionary = new Dictionary<int, Item>(); // Dictionary of available items

    [Header("Menus")]
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject clothesShopMenu;
    [SerializeField] private GameObject clothesBuyerMenu;
    [SerializeField] private GameObject inventoryMenu;
    
    [Header("Tooltips")]
    [SerializeField] private GameObject interactionTooltip;

    private void Awake()
    {
        // Singleton implementation
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(this);
        }
        
        // Cache list of item prefabs
        ItemsDictionary = FindAvailableItems();
    }

    private void Start()
    {
        // Make sure UIs are not showing on startup
        pauseMenu.SetActive(false);
        clothesShopMenu.SetActive(false);
        clothesBuyerMenu.SetActive(false);
        inventoryMenu.SetActive(false);
        interactionTooltip.SetActive(false);
        
        // Hide cursor
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }

    public void LoadMainMenu()
    {
        // If haven't tried to load anything yet, return
        if (_asyncOperation != null) return;
        
        // Load Main Menu
        _asyncOperation = SceneManager.LoadSceneAsync("MainMenu");
        _asyncOperation.allowSceneActivation = true;
        
        // Unpause the game
        isPaused = false;
        Time.timeScale = 1.0f;
    }

    public void Pause(bool paused)
    {        
        if (paused)
        {
            // Pause the game
            isPaused = true;
            Time.timeScale = 0f;
            
            // Show cursor
            ToggleCursor(true);
            
            // Show Pause Menu UI
            pauseMenu.SetActive(true);
            
            // Hide other UIs
            interactionTooltip.SetActive(false);
            clothesShopMenu.SetActive(false);
            clothesBuyerMenu.SetActive(false);
            inventoryMenu.SetActive(false);
        }
        else
        {
            // Unpause the game
            isPaused = false;
            Time.timeScale = 1.0f;
            
            // Hide cursor
            ToggleCursor(false);
            
            // Hide Pause Menu UI
            pauseMenu.SetActive(false);
            
            // If player was in interaction range, show Interaction Tooltip again
            if (isInInteractionRange)
                ToggleInteractionTooltip(true, canInteractWithSeller, canInteractWithBuyer);
            
            // If player had the Clothes Shop open before pausing, open it again
            if (isClothesShopOpen)
                ToggleClothesShop(true);
            
            // If player had the Clothes Buyer open before pausing, open it again
            if (isClothesBuyerOpen)
                ToggleClothesBuyer(true);
            
            // If player had the Inventory open before pausing, open it again
            if (isInventoryOpen)
                ToggleInventory(true);
        }
    }

    public void ToggleCursor(bool active)
    {
        Cursor.lockState = active ? CursorLockMode.None : CursorLockMode.Confined;
        Cursor.visible = active;
    }

    public void ToggleInteractionTooltip(bool active, bool isSeller, bool isBuyer)
    {
        isInInteractionRange = active;
        canInteractWithSeller = isSeller;
        canInteractWithBuyer = isBuyer;
        interactionTooltip.SetActive(active);
    }

    public void ToggleClothesShop(bool active)
    {
        ToggleCursor(active);
        isClothesShopOpen = active;
        clothesShopMenu.SetActive(active);
    }
    
    public void ToggleClothesBuyer(bool active)
    {
        ToggleCursor(active);
        isClothesBuyerOpen = active;
        clothesBuyerMenu.SetActive(active);
    }

    public void ToggleInventory(bool active)
    {
        ToggleCursor(active);
        isInventoryOpen = active;
        inventoryMenu.SetActive(active);
    }
    
    // Set available Items
    private Dictionary<int, Item> FindAvailableItems()
    {
        ItemsDictionary[0] = null;
        // Creates a dictionary out of all item prefabs
        foreach (Item item in itemPrefabs)
        {
            if (item != null)
                ItemsDictionary[item.itemId] = item;
        }
        return ItemsDictionary;
    }
}
