using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject controlsMenu;
    private AsyncOperation _asyncOperation;
    private void Start()
    {
        // Make sure cursor is visible in the Main Menu
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        
        // Make sure main menu GameObject is active when opening the Main Menu
        mainMenu.SetActive(true);
        controlsMenu.SetActive(false);
    }

    // Load game scene
    public void StartGame()
    {
        // If haven't tried to load anything yet, return
        if (_asyncOperation != null) return;

        // Restore the current scene
        Scene currentScene = SceneManager.GetActiveScene();
        // Load next scene
        _asyncOperation = SceneManager.LoadSceneAsync(currentScene.buildIndex + 1);
        _asyncOperation.allowSceneActivation = true;
    }

    public void Exit()
    {
        #if UNITY_STANDALONE
        Application.Quit();
        #endif

        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}