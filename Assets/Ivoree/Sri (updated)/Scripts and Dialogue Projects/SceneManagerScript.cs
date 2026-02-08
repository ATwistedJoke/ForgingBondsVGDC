using UnityEngine;
using UnityEngine.SceneManagement;
using Yarn.Unity;

public class SceneManagerScript : MonoBehaviour
{

    private DialogueRunner dialogueRunner;

    void Awake()
    {
        
        dialogueRunner = GetComponent<DialogueRunner>();

        dialogueRunner.AddCommandHandler<string>(
            "load_scene",
            LoadScene
        );

        Debug.Log("load_scene command registered");
    }

    private void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
