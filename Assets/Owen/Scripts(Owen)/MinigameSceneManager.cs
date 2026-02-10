using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Yarn.Unity;

public class MinigameSceneManager : MonoBehaviour
{
    public DialogueRunner dialogueRunner; 
    public GameObject minigamePrefab; 
    public void Awake()
    {
        DontDestroyOnLoad(dialogueRunner); 
        dialogueRunner.AddCommandHandler<string>(
            "load_scene",     // the name of the command
            LoadScene // the method to run
        );
        dialogueRunner.AddCommandHandler<int>(
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

    void LoadScene(string name)
    {
        SceneManager.LoadScene(name); 
    }
    void InstantiatePrefab(int idx)
    {
        Instantiate(minigamePrefab); 
    }
}
