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

    public GameObject blackBackground;
    public GameObject kingdom;
    public GameObject entranceHall;
    public GameObject hallway;
    public GameObject personalForge;
    public GameObject commissionsRoomDay;
    public GameObject commissionsRoomNight;

    //Character Handling
    public GameObject[] spPrefab = new GameObject[5]; 
    public GameObject[] sprite = new GameObject[5];

    int mentorAffinity = 0;
    int redFlagAffinity = 0;
    int bestFriendAffinity = 0;
    int loneWolfAffinity = 0;

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

        dialogueRunner.AddCommandHandler<int,int,int>(
            "instance_sprite",
            InstantiateChar
        );

        dialogueRunner.AddCommandHandler<int,int>(
            "change_sprite",
            SpriteChange
        );

        dialogueRunner.AddCommandHandler<int,int,int>(
            "move_sprite",
            MoveChar
        );

        dialogueRunner.AddCommandHandler<int>(
            "destroy_sprite",
            DestroyChar
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
            case "black background":
                Instantiate(blackBackground);
                break;
            case "entrance hall":
                Instantiate(entranceHall);
                break;
            case "kingdom":
                Instantiate(kingdom);
                break;
            case "hallway":
                Instantiate(hallway);
                break;
            case "personal forge":
                Instantiate(personalForge);
                break;
            case "commissions room daytime":
                Instantiate(commissionsRoomDay);
                break;
            case "commissions room nighttime":
                Instantiate(commissionsRoomNight);
                break;
            case "empty":
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
        else if(character == "lone wolf")
        {
            loneWolfAffinity += modifier;
            Debug.Log("Affinity for "+ character + " changed to " + loneWolfAffinity);
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

    //Sprite Methods
    private void InstantiateChar(int idx, int posX, int posY)
    {
        sprite[idx] = Instantiate(spPrefab[idx]);
        sprite[idx].transform.position = new Vector2(posX, posY);  
    }
    private void SpriteChange(int oIdx, int sIdx)
    {
        CharacterManager image = sprite[oIdx].GetComponent<CharacterManager>(); 
        image.ChangeSprite(sIdx); 
    }
    private void MoveChar(int idx, int posX, int posY)
    {
        Vector3 target = new Vector3(posX,posY,5);
        GameObject obj = sprite[idx]; 
        StartCoroutine(MoveOverTime(obj,target,1));
    }
    private void DestroyChar(int idx)
    {
        Destroy(sprite[idx]);
        sprite[idx] = null; 
    }

    private IEnumerator MoveOverTime(GameObject obj, Vector3 target, float spd)
    {
        while(obj != null && obj.transform.position != target)
        {
            obj.transform.position = Vector3.MoveTowards(obj.transform.position, target, spd*Time.deltaTime); 
            yield return new WaitForEndOfFrame(); 
        }
    }



}
