using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using Yarn.Unity;

public class GameManger : MonoBehaviour
{
    // Drag and drop your Dialogue Runner into this variable.
    public DialogueRunner dialogueRunner;

    [Header("Minigame Prefabs")]
    public GameObject tutorialMinigame;
    public GameObject resourceMinigame;
    public GameObject smeltingMinigame;

    public GameObject entranceHall;
    public GameObject hallway;
    public GameObject personalForge;
    public GameObject commissionsRoom;


    int mentorAffinity = 0;
    int redFlagAffinity = 0;
    int bestFriendAffinity = 0;

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

        dialogueRunner.AddCommandHandler<string, string>(
            "run_minigame",
            RunMinigame
        );

        dialogueRunner.AddCommandHandler<string>(
            "change_background",
            ChangeBackground
        );
    }

    private void LoadScene(string sceneName) 
    {
        SceneManager.LoadScene(sceneName);
    }

    private void ChangeBackground(string newBackground)
    {
        foreach(GameObject background in GameObject.FindGameObjectsWithTag("background"))
        {
            Destroy(background);
        }
        
        switch (newBackground)
        {
            case "entrance hall":
                Instantiate(entranceHall);
                break;
            case "hallway":
                Instantiate(hallway);
                break;
            case "personal forge":
                Instantiate(personalForge);
                break;
            case "commissions room":
                Instantiate(commissionsRoom);
                break;
        }
    }
    private void ChangeAffinity(string character, int modifier)
    {
        if(character == "mentor")
        {
            mentorAffinity += modifier;
            Debug.Log("Affinity for "+ character + " changed to " + mentorAffinity);
        }
        else if(character == "red flag")
        {
            redFlagAffinity += modifier;
            Debug.Log("Affinity for "+ character + " changed to " + redFlagAffinity);
        }
        else if(character == "best friend")
        {
            bestFriendAffinity += modifier;
            Debug.Log("Affinity for "+ character + " changed to " + bestFriendAffinity);
        }
    }

    private void RunMinigame(string minigameID, string dialogueNode)
    {
        StartCoroutine(RunMinigameCoroutine(minigameID, dialogueNode));
    }

    private IEnumerator RunMinigameCoroutine(string minigameID, string dialogueNode)
    {

        yield return null;

        dialogueRunner.Stop();

        GameObject prefabToSpawn = null;

        switch (minigameID)
        {
            case "tutorialMinigame":
                prefabToSpawn = tutorialMinigame;
                break;
            case "resourceMinigame":
                prefabToSpawn = resourceMinigame;
                break;
            case "smeltingMinigame":
                prefabToSpawn = smeltingMinigame;
                break;
        }

        if(prefabToSpawn == null)
        {
            Debug.LogError("Minigame not found: " + minigameID);
            yield break;
        }

        Instantiate(prefabToSpawn);
        

        while(GameObject.FindGameObjectsWithTag("minigame").Length > 0)
        {
            yield return null;
        }

        dialogueRunner.StartDialogue(dialogueNode);
    }




}
