using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void OnStartClick()
    {
        // Load the main game scene
        SceneManager.LoadScene("DialogueCreation");
    }

    public void OnMinigameClick()
    {
        SceneManager.LoadScene("SequenceoftheMinigames");
    }
    public void OnSettingsClick()
    {
        // Load the settings scene
        SceneManager.LoadScene("Settings");
    }

    public void OnReturn()
    {
        SceneManager.LoadScene("MainMenu");
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
