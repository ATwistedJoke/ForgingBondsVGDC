using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using Yarn.Unity;

public class SceneCommands : MonoBehaviour
{
    // Drag and drop your Dialogue Runner into this variable.
    public DialogueRunner dialogueRunner;

    int mentorAffinity = 0;

    public void Awake() {

        DontDestroyOnLoad(dialogueRunner);
        
        dialogueRunner.AddCommandHandler<string>(
            "load_scene",     // the name of the command
            LoadScene // the method to run
        );

        dialogueRunner.AddCommandHandler<string, int>(
            "change_affinity",
            ChangeAffinity
        );
    }

    private void LoadScene(string sceneName) 
    {
        SceneManager.LoadScene(sceneName);
    }

    private void ChangeAffinity(string character, int modifier)
    {
        if(character == "mentor")
        {
            mentorAffinity += modifier;
            Debug.Log("Affinity for "+ character + " changed to " + mentorAffinity);
        }
    }


}
