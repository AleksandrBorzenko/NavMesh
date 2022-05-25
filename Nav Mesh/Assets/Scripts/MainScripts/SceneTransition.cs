using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// This script manages a change of the scenes
/// </summary>
public class SceneTransition : MonoBehaviour
{
    private readonly string loadSceneName = "Load";

    private int delay = 2000;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// Loads the scene with transition
    /// </summary>
    public void LoadSceneWithTransition(string nextScene)
    {
        SceneManager.LoadSceneAsync(loadSceneName);
        LoadNewScene(nextScene);
    }

    /// <summary>
    /// Loads a game or menu scene with delay in load scene
    /// </summary>
    /// <param name="nextScene">Next scene name</param>
    private async void LoadNewScene(string nextScene)
    {
        await Task.Delay(delay); // Here can be an initializing process
        SceneManager.LoadSceneAsync(nextScene);
        Destroy(gameObject);
    }
    
}
