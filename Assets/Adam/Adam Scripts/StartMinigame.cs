using UnityEngine;

public class StartMinigame : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject minigame;
    public Transform spawnPoint; 

    public void MinigameStart()
    {
        if(minigame != null && spawnPoint != null)
        {
            Instantiate(minigame, spawnPoint.transform.position, spawnPoint.transform.rotation);
        }
        if(minigame != null && spawnPoint == null)
        {
            Instantiate(minigame);
        }
    }
}
