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
    public GameObject[] prefabList; 

    public GameObject blackBackground;
    public GameObject map;
    public GameObject entranceHall;
    public GameObject hallway;
    public GameObject personalForge;
    public GameObject commissionsRoomDay;
    public GameObject commissionsRoomNight;
    public GameObject banquet;
    public GameObject restaurant;
    public GameObject maeveRoom;
    public GameObject mines;
    public GameObject market;
    public GameObject lake;
    public GameObject capitalStreets;
    public GameObject judithsHome;

    //Character Handling
    public GameObject[] spPrefab = new GameObject[5]; 
    public GameObject[] sprite = new GameObject[5];

    // int mentorAffinity = 0;
    // int redFlagAffinity = 0;
    // int bestFriendAffinity = 0;
    // int loneWolfAffinity = 0;
    // int corruptionValue = 0;

    //store the minigame score here. 0 = bad, 1 = mediocre, 2 = good
    int resourceMinigameScore = 2;
    int smeltingMinigameScore = 2;

    public void Awake() {

        DontDestroyOnLoad(dialogueRunner);
        
        dialogueRunner.AddCommandHandler<string>(
            "load_scene",     // the name of the command
            LoadScene // the method to run
        );

        // dialogueRunner.AddCommandHandler<string, int>(
        //     "change_affinity",
        //     ChangeAffinity
        // );

        dialogueRunner.AddCommandHandler<int, string>(
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

        dialogueRunner.AddCommandHandler<int,int,int,int>(
            "move_sprite",
            MoveChar
        );

        dialogueRunner.AddCommandHandler<int>(
            "destroy",
            DestroyChar
        );

        dialogueRunner.AddCommandHandler(
            "resource_result",
            ResourceMinigameResult
        );

        dialogueRunner.AddCommandHandler(
            "smelting_result",
            SmeltingMinigameResult
        );

        dialogueRunner.AddCommandHandler<bool>(
            "MC",
            MCSpeak
        );

        // dialogueRunner.AddCommandHandler<int>(
        //     "change_corruption",
        //     ChangeCorruption
        // );
    }

    private IEnumerator ResourceMinigameResult()
    {
        yield return null;

        dialogueRunner.Stop();

        if(resourceMinigameScore == 0)
        {
            dialogueRunner.StartDialogue("resourcegameBad");
        }
        else if(resourceMinigameScore == 1)
        {
            dialogueRunner.StartDialogue("resourcegameMediocre");
        }
        else if(resourceMinigameScore == 2)
        {
            dialogueRunner.StartDialogue("resourcegameGood");
        }

        //at the end of the dialogue node that we switch to, we have to jump back 
        // into the main dialogue line that doesn't depend on the minigame score
    }

    private IEnumerator SmeltingMinigameResult()
    {
        yield return null;

        dialogueRunner.Stop();

        if(smeltingMinigameScore == 0)
        {
            dialogueRunner.StartDialogue("smeltinggameBad");
        }
        else if(smeltingMinigameScore == 1)
        {
            dialogueRunner.StartDialogue("smeltinggameMediocre");
        }
        else if(smeltingMinigameScore == 2)
        {
            dialogueRunner.StartDialogue("smeltinggameGood");
        }

        //at the end of the dialogue node that we switch to, we have to jump back 
        // into the main dialogue line that doesn't depend on the minigame score
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
            case "map":
                Instantiate(map);
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
            case "banquet":
                Instantiate(banquet);
                break;
            case "restaurant":
                Instantiate(restaurant);
                break;
            case "maeve's room":
                Instantiate(maeveRoom);
                break;
            case "mines":
                Instantiate(mines);
                break;
            case "market":
                Instantiate(market);
                break;
            case "lake":
                Instantiate(lake);
                break;
            case "capital streets":
                Instantiate(capitalStreets);
                break;
            case "Judith's home":
                Instantiate(judithsHome);
                break;
            
        }
    }
    // private void ChangeAffinity(string character, int modifier)
    // {
    //     if(character == "mentor")
    //     {
    //         mentorAffinity += modifier;
    //         Debug.Log("Affinity for "+ character + " changed to " + mentorAffinity);
    //     }
    //     else if(character == "red flag")
    //     {
    //         redFlagAffinity += modifier;
    //         Debug.Log("Affinity for "+ character + " changed to " + redFlagAffinity);
    //     }
    //     else if(character == "best friend")
    //     {
    //         bestFriendAffinity += modifier;
    //         Debug.Log("Affinity for "+ character + " changed to " + bestFriendAffinity);
    //     }
    //     else if(character == "lone wolf")
    //     {
    //         loneWolfAffinity += modifier;
    //         Debug.Log("Affinity for "+ character + " changed to " + loneWolfAffinity);
    //     }
    // }

    // private void ChangeCorruption(int modifier)
    // {
    //     corruptionValue += modifier;
    // }

    // private void CheckCorruption()
    // {
    //     if(corruptionValue == 1)
    // }

    private void RunMinigame(int idx, string dialogueNode)
    {
        StartCoroutine(RunMinigameCoroutine(idx, dialogueNode));
    }

    private IEnumerator RunMinigameCoroutine(int idx, string dialogueNode)
    {
        yield return null;

        dialogueRunner.Stop();

        GameObject prefabToSpawn = prefabList[idx];

        if(prefabToSpawn == null)
        {
            Debug.LogError("Minigame not found");
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
    private void MoveChar(int idx, int posX, int posY, int speed)
    {
        Vector3 target = new Vector3(posX,posY,0);
        GameObject obj = sprite[idx]; 
        StartCoroutine(MoveOverTime(obj,target,speed));
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
        Debug.Log("Done");
    }

    private void MCSpeak(bool speak)
    {
        Vector3 target; 
        if (speak)
        {
            target = new Vector3(-7,-6,0);
        }
        else
        {
            target = new Vector3(-50,-6,0);
        }
        StartCoroutine(MoveOverTime(sprite[0],target,50));
    }
}
