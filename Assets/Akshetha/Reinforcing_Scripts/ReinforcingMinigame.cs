using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class ReinforcingMinigame : MonoBehaviour
{
    [Header("Minigame Settings")]
    [SerializeField] public float totalTimeLimit = 60f; // time
    [SerializeField] private int maxQualityScore = 100; //score
    
    [Header("UI References")]
    [SerializeField] private GameObject minigameCanvas; //gaming area
    [SerializeField] private Image shieldImage; 
    [SerializeField] private Image qualityMeterFill; 
    [SerializeField] private TextMeshProUGUI qualityScoreText;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private GameObject challengePanel;
    [SerializeField] private TextMeshProUGUI challengeInstructionText;
    [SerializeField] private Image challengeProgressBar;
    
    [Header("Shield Segments")]
    [SerializeField] private List<ShieldSegment> segments = new List<ShieldSegment>(); 
    
    [Header("Challenge Settings")]
    [SerializeField] private float firsttimeWindow = 5f; // press & release
    [SerializeField] private int MinPresses = 5;
    [SerializeField] private int MaxPresses2 = 15;
    [SerializeField] private float type1TimeLimit = 10f;

    [SerializeField] private float timeLimit2 = 3f;
    
    // call back to gamemanager
    public Action<int> OnMinigameComplete;
    
    // current state
    public float currentTime;
    private int qualityScore = 0;
    private int segmentsCompleted = 0;
    private bool isMinigameActive = false;
    private ShieldSegment currentSegment;
    
    // Challenge state
    private bool isChallengeActive = false;
    private int currentChallengeType; 
    private float challengeTimer;
    private int type1SuccessfulPresses = 0;
    private int type1RequiredPresses = 5; 
    private float type1PressTime;
    private int type2PressCount;
    private int type2RequiredPresses;
    
    public void Start()
    {
        isMinigameActive = true;
        currentTime = totalTimeLimit;
        qualityScore = 0;
        segmentsCompleted = 0;
        
        minigameCanvas.SetActive(true);
        challengePanel.SetActive(false);
        
        
        UpdateQualityUI();
        UpdateTimerUI();
    }
    
    private void Update()
    {
        if (!isMinigameActive) return;
        
        // Update main timer UUGGHHHGHGGHSGSHYGSU
        currentTime -= Time.deltaTime;
        UpdateTimerUI();
        
        if (currentTime <= 0)
        {
            EndMinigame();
            return;
        }
        
        // Handle challenge input
        if (isChallengeActive)
        {
            HandleChallengeInput();
        }
    }
    

    public void OnSegmentClicked(ShieldSegment segment)
    {
        if (!isMinigameActive || isChallengeActive || segment.IsCompleted)
            return;
        
        currentSegment = segment;
        StartRandomChallenge();
    }

    

    //
    private void StartRandomChallenge()
    {
        currentChallengeType = UnityEngine.Random.Range(1, 3);
        
        isChallengeActive = true;
        challengePanel.SetActive(true);
        
        if (currentChallengeType == 1)
        {
            StartType1Challenge();
        }
        else
        {
            StartType2Challenge();
        }
    }
    
//hold and release
    
    private void StartType1Challenge()
    {
        challengeTimer = type1TimeLimit;
        type1SuccessfulPresses = 0;
        challengeInstructionText.text = $"Press SPACE when the color changes\n{type1SuccessfulPresses}/{type1RequiredPresses} successful presses\nTime left: {challengeTimer:F1}s";
        StartCoroutine(Type1ProgressAnimation()); //ask adam
        StartCoroutine(Type1TimerCountdown()); 
    }
    
    private IEnumerator Type1ProgressAnimation()
    {
        while (isChallengeActive && currentChallengeType == 1)
        {
            float elapsed = 0f;
            while (elapsed < firsttimeWindow && isChallengeActive && currentChallengeType == 1)
            {
                elapsed += Time.deltaTime;
                float progress = elapsed / firsttimeWindow;
                
                if (challengeProgressBar != null)
                {
                    challengeProgressBar.fillAmount = progress;
                    
                    float perfectStart = 0.45f;
                    float perfectEnd = 0.59f;
                    
                    if (progress >= perfectStart && progress <= perfectEnd)
                    {
                        challengeProgressBar.color = Color.pink;
                    }
                    else
                    {
                        challengeProgressBar.color = Color.red;
                    }
                }
                
                yield return null;
            }
        }
    }
    
    private void HandleType1Input()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            Debug.Log("b");
            float barProgress = challengeProgressBar.fillAmount;
            
            float perfectStart = 0.45f;
            float perfectEnd = 0.59f;
            
            if (barProgress >= perfectStart && barProgress <= perfectEnd)
            {
                type1SuccessfulPresses++;
                UpdateGame1Text();
                
                if (type1SuccessfulPresses >= type1RequiredPresses)
                {
                    CompleteChallenge(true);
                }
            }
            else
            {
                challengeTimer = 0;
            }
        }
    }
    
    private void UpdateGame1Text()
    {
        challengeInstructionText.text = $"Press SPACE when the color changes \n{type1SuccessfulPresses}/{type1RequiredPresses} successful presses\nTime left: {challengeTimer:F1}s";
    }

    private IEnumerator Type1TimerCountdown()
    {
        while (isChallengeActive && currentChallengeType == 1 && challengeTimer > 0)
        {
            challengeTimer -= Time.deltaTime;
            UpdateGame1Text();
            yield return null;
        }
        
        // Time ran out
        if (challengeTimer <= 0 && isChallengeActive && currentChallengeType == 1)
        {
            FailChallenge();
        }
    }



    
    //smashorpass
    private void StartType2Challenge()
    {
        type2RequiredPresses = UnityEngine.Random.Range(MinPresses, MaxPresses2 + 1);
        type2PressCount = 0;
        challengeTimer = timeLimit2;
                
        challengeInstructionText.text = $"MASH SPACEBAR!!!\n{type2PressCount}/{type2RequiredPresses} presses";
        
        if (challengeProgressBar != null)
        {
            challengeProgressBar.fillAmount = 0f;
            challengeProgressBar.color = Color.yellow;
        }
        StartCoroutine(Type2TimerCountdown());
    }

    private IEnumerator Type2TimerCountdown()
    {
        //timer change
        while (isChallengeActive && currentChallengeType == 2 && challengeTimer > 0)
        {
            challengeTimer -= Time.deltaTime;
            UpdateGame2Text();
            yield return null;
        }
        
        // time gone
        if (challengeTimer <= 0 && isChallengeActive && currentChallengeType == 2)
        {
            FailChallenge();
        }
    }
    private void HandleType2Input()
    {
        // Detect button presses
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            type2PressCount++;
            UpdateGame2Text();
            
            if (type2PressCount >= type2RequiredPresses)
            {
                CompleteChallenge(true);
            }
        }
    }
    
    private void UpdateGame2Text()
    {
        challengeInstructionText.text = $"MASH SPACEBAR!!!\n{type2PressCount}/{type2RequiredPresses} presses\nTime left: {challengeTimer:F1}s";
        
        if (challengeProgressBar != null)
        {
            challengeProgressBar.fillAmount = (float)type2PressCount / type2RequiredPresses;
        }
    }
    
    
    private void HandleChallengeInput()
    {
        if (currentChallengeType == 1) 
        {
            HandleType1Input();
        }
        else if (currentChallengeType == 2)
        {
            HandleType2Input();
        }
    }
    
    private void CompleteChallenge(bool success)
    {
        isChallengeActive = false;
        StopAllCoroutines();
        
        if (success)
        {
            // mark as complete
            currentSegment.MarkCompleted();
            segmentsCompleted++;
            
            // quality score increment
            int scoreGain = maxQualityScore / segments.Count;
            qualityScore = Mathf.Min(qualityScore + scoreGain, maxQualityScore);
            
            UpdateQualityUI();
            
            // check if all completed
            if (segmentsCompleted >= segments.Count)
            {
                EndMinigame();
                return;
            }
        }
        
        challengePanel.SetActive(false);
    }
    
    private void FailChallenge()
    {
        CompleteChallenge(false);
    }
    
    
    private void UpdateQualityUI()
    {
        if (qualityMeterFill != null)
        {
            qualityMeterFill.fillAmount = (float)qualityScore / maxQualityScore;
        }
        
        if (qualityScoreText != null)
        {
            qualityScoreText.text = $"Quality: {qualityScore}/{maxQualityScore}";
        }
    }
    
    private void UpdateTimerUI()
    {
        if (timerText != null)
        {
            timerText.text = $"Time: {Mathf.CeilToInt(currentTime)}s";
        }
    }
    



    
    private void EndMinigame()
    {
        isMinigameActive = false;
        minigameCanvas.SetActive(false);
        
        OnMinigameComplete?.Invoke(qualityScore);
        //Destroy(gameObject);
        GameObject rem = GameObject.FindGameObjectWithTag("minigame");
        Destroy(rem);
    }
    
}