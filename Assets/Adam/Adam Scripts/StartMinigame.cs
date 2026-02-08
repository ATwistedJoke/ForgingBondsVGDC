using UnityEngine;

public class StartMinigame : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject minigame;
    public GameObject timer;
    public Transform spawnPoint; 
    public Transform CanvasTransform;
    public GameObject tutorialVideo;
    void Start(){
        tutorialVideo = GameObject.FindGameObjectWithTag("Tutorial Video");
    }
    public void MinigameStart()
    {
        if(minigame != null && spawnPoint != null)
        {
            Instantiate(minigame, spawnPoint.transform.position, spawnPoint.transform.rotation);
        }
        else if(minigame != null && CanvasTransform != null)
        {
            Instantiate(minigame, CanvasTransform);
        }
        else if(minigame != null && spawnPoint == null)
        {
            Instantiate(minigame);
        }
        if(timer != null && CanvasTransform != null){
            GameObject Timer = Instantiate(timer, CanvasTransform);
            Timer.transform.SetParent(CanvasTransform);
        }
        if(tutorialVideo != null){
            Destroy(tutorialVideo);
        }
        Destroy(gameObject);
    }
}
