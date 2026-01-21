using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using Yarn.Unity;

public class SceneCommands : MonoBehaviour
{
    // Drag and drop your Dialogue Runner into this variable.
    public DialogueRunner dialogueRunner;

    public void Awake() {
        
        DontDestroyOnLoad(dialogueRunner);
        // Create a new command called 'camera_look', which looks at a target. 
        // Note how we're listing 'GameObject' as the parameter type.
        dialogueRunner.AddCommandHandler<string>(
            "load_scene",     // the name of the command
            LoadScene // the method to run
        );
    }

    // The method that gets called when '<<camera_look>>' is run.
    private void LoadScene(string sceneName) 
    {
        SceneManager.LoadScene(sceneName);
    }


}
