using UnityEngine;

public class ReinforcingGameManager : MonoBehaviour
{
    [Header("Minigame Prefab")]
    [SerializeField] private GameObject reinforcingMinigamePrefab;
    
    private ReinforcingMinigame currentMinigame;
    
    public void StartReinforcingMinigame()
    {
        GameObject minigameObj = Instantiate(reinforcingMinigamePrefab);
        currentMinigame = minigameObj.GetComponent<ReinforcingMinigame>();
        
        if (currentMinigame != null)
        {
            currentMinigame.OnMinigameComplete += OnMinigameCompleted;
            currentMinigame.Start();
        }
        else
        {
        }
    }

    private void OnMinigameCompleted(int finalScore)
    {
        if (finalScore >= 80)
        {
            GameManager.instance.GiveResult(2);
        }
        else if (finalScore >= 50)
        {
            GameManager.instance.GiveResult(1);
        }
        else
        {
            GameManager.instance.GiveResult(0); 
        }
    }
    
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            StartReinforcingMinigame();
        }
    }
}