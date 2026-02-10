using System;
using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using Yarn.Unity;

public class MinigameSceneManager : MonoBehaviour
{
    public DialogueRunner dr; 
    public GameObject minigamePrefab; 
    public bool isLoading = false;
    public void Awake()
    {
        DontDestroyOnLoad(dr); 
        dr.AddCommandHandler<string, string>(
            "load_scene",     // the name of the command
            LoadScene // the method to run
        );
        dr.AddCommandHandler<int>(
            "instance",
            InstantiatePrefab
        );
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator LoadScene(string name, string dialogueNode)
    {
        yield return null;

        isLoading = true;

        dr.Stop();

        SceneManager.LoadScene(name); 

        isLoading = false;

        while(GameObject.FindGameObjectsWithTag("minigame").Length > 0 && isLoading == false)
        {
            yield return null;
        }

        dr.StartDialogue(dialogueNode);
    }
    void InstantiatePrefab(int idx)
    {
        Instantiate(minigamePrefab); 
    }
}
