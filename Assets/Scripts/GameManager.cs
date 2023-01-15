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

    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject clothesShopMenu;
    [SerializeField] private GameObject inventoryMenu;
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
    }

    private void Start()
    {
        // Make sure UIs are not showing on startup
        pauseMenu.SetActive(false);
        clothesShopMenu.SetActive(false);
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
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            
            // Show Pause Menu UI
            pauseMenu.SetActive(true);
        }
        else
        {
            // Unpause the game
            isPaused = false;
            Time.timeScale = 1.0f;
            
            // Hide cursor
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = false;
            
            // Hide Pause Menu UI
            pauseMenu.SetActive(false);
        }
    }
}
