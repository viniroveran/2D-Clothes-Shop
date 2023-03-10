using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour
{
    [SerializeField] private float delay = 2.0f;
    [SerializeField] private int sceneToLoad = -1;

    private AsyncOperation _asyncOperation;
    private bool _ready = false;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1.0f;
        Input.ResetInputAxes();
        System.GC.Collect();
        Scene currentScene = SceneManager.GetActiveScene();
        _asyncOperation = sceneToLoad <= -1 ? SceneManager.LoadSceneAsync(currentScene.buildIndex + 1) : SceneManager.LoadSceneAsync(sceneToLoad);
        _asyncOperation.allowSceneActivation = false;

        // Load the next scene after a delay
        Invoke(nameof(Activate), delay);
    }

    public void Activate()
    {
        _ready = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (_asyncOperation.progress >= 0.89f && SplashScreen.isFinished && _ready)
        {
            _asyncOperation.allowSceneActivation = true;
        }
    }
}
