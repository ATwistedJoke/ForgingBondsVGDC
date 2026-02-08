using UnityEngine;

public class StartMinigame : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject minigame;
    public Transform spawnPoint; 
    public Transform CanvasTransform;
    public void MinigameStart()
    {
        if(minigame != null && CanvasTransform != null)
        {
            Instantiate(minigame, CanvasTransform);
        }
        else if(minigame != null && spawnPoint != null)
        {
            Instantiate(minigame, spawnPoint.transform.position, spawnPoint.transform.rotation);
        }
        else if(minigame != null && spawnPoint == null)
        {
            Instantiate(minigame);
        }
    }
}
