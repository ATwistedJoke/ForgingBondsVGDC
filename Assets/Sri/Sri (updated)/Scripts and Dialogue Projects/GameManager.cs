using System;
using System.Collections;
using FMODUnity;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using Yarn;
using Yarn.Unity;

public class GameManager : MonoBehaviour
{
    public static GameManager instance{get; private set;}
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
    public GameObject silasHome;
    public GameObject village;
    public GameObject trainingGrounds;
    public GameObject manor;
    public GameObject badlands;
    public GameObject battlefield;


    //Character Handling
    public GameObject[] spPrefab = new GameObject[5]; 
    public GameObject[] sprite = new GameObject[5];
    private int MCAppearance; 
    private int xPosition = 0; 
    private int yPosition = -4; 

    //store the minigame score here. 0 = bad, 1 = mediocre, 2 = good
    int resourceMinigameScore = 2;
    int smeltingMinigameScore = 2;

    public InMemoryVariableStorage variableStore;  

    public void Awake() {
        if(instance != null && instance != this)
        {
            Destroy(this);  
        }
        instance = this; 
        DontDestroyOnLoad(dialogueRunner);

        variableStore = FindAnyObjectByType<InMemoryVariableStorage>(); 
        
        dialogueRunner.AddCommandHandler<string>(
            "load_scene",     // the name of the command
            LoadScene // the method to run
        );
        // dialogueRunner.AddCommandHandler<string, int>(
        //     "change_affinity",
        //     ChangeAffinity
        // );
        dialogueRunner.AddCommandHandler<int, string>("run_minigame", RunMinigame);
        dialogueRunner.AddCommandHandler<string>("change_background", ChangeBackground);
        dialogueRunner.AddCommandHandler<int>("sp", InstantiateChar);
        dialogueRunner.AddCommandHandler<int, int, int>("place", InstantiatePlace);
        dialogueRunner.AddCommandHandler<int,int>("cs", SpriteChange);
        dialogueRunner.AddCommandHandler<int,int,int,int>("mv", MoveChar);
        dialogueRunner.AddCommandHandler<int>("destroy", DestroyChar);
        dialogueRunner.AddCommandHandler("resource_result", ResourceMinigameResult);
        dialogueRunner.AddCommandHandler("smelting_result", SmeltingMinigameResult);
        dialogueRunner.AddCommandHandler<bool>("MC",MCSpeak);
        dialogueRunner.AddCommandHandler<int>("Appearance", SetAppearance);
        dialogueRunner.AddCommandHandler("SFX", PlayAudio);
        dialogueRunner.AddCommandHandler<int,int>("Voice", VoiceLine);
        dialogueRunner.AddCommandHandler<float>("Theme", ChangeTheme);
        // dialogueRunner.AddCommandHandler<int>("change_corruption",ChangeCorruption);
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
            case "Silas's home":
                Instantiate(silasHome);
                break;
            case "village":
                Instantiate(village);
                break;
            case "training grounds":
                Instantiate(trainingGrounds);
                break;
            case "manor":
                Instantiate(manor);
                break;
            case "badlands":
                Instantiate(badlands);
                break;
            case "battlefield":
                Instantiate(battlefield);
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

    //Result Handling
    public void GiveResult(int result)
    {
        variableStore.SetValue("$resultTest", result);
    }

    //Sprite Methods
    private void InstantiateChar(int idx)
    {
        if(idx == 0){ idx = MCAppearance; }
        sprite[idx] = Instantiate(spPrefab[idx]);
        sprite[idx].transform.position = new Vector2(xPosition, yPosition);  
    }

    private void InstantiatePlace(int idx, int posX, int posY)
    {
        if(idx == 0){ idx = MCAppearance; }
        sprite[idx] = Instantiate(spPrefab[idx]);
        sprite[idx].transform.position = new Vector2(posX, posY);
    }
    private void SpriteChange(int oIdx, int sIdx)
    {
        if(oIdx == 0){ oIdx = MCAppearance; }
        CharacterManager image = sprite[oIdx].GetComponent<CharacterManager>(); 
        image.ChangeSprite(sIdx); 
    }
    private void MoveChar(int idx, int posX, int posY, int speed)
    {
        sprite[idx].GetComponentInChildren<CharacterManager>().Move(posX, posY, speed);
    }
    private void DestroyChar(int idx)
    {
        if(idx == 0){ idx = MCAppearance; }
        Destroy(sprite[idx]);
        sprite[idx] = null; 
    }

    private void SetAppearance(int idx)
    {
        MCAppearance = idx; 
    }
    private void MCSpeak(bool speak)
    {
        if (speak)
        {
            sprite[MCAppearance].GetComponentInChildren<CharacterManager>().Move(-7, -6, 100);
        }
        else
        {
            sprite[MCAppearance].GetComponentInChildren<CharacterManager>().Move(-30, -6, 100);
        }
    }

    //Audio Implementation
    public void PlayAudio()
    {
        AudioManager.instance.PlayOneShot(FMODEvents.instance.SFX[0], transform.position);
    }

    public void VoiceLine(int idx, int line)
    {
        sprite[idx].GetComponentInChildren<CharacterManager>().PlayLine(line);
    }

    public void ChangeTheme(float f)
    {
        AudioManager.instance.SetMusicArea(f);
    }
}
