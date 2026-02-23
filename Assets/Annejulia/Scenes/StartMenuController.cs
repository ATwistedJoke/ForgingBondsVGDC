using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class NewMonoBehaviourScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void OnStartClick()
    {
        // Load the main game scene
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainGameScene");
    }

    public void OnSettingsClick()
    {
        // Load the settings scene
        UnityEngine.SceneManagement.SceneManager.LoadScene("Setting");
    }

    public void OnExitClick()
    {
#if UNITY_EDITOR
    UnityEditor.EditorApplication.isPlaying = false; // Stop play mode in the editor
#endif
        // Exit the application
        Application.Quit();
    }
}
