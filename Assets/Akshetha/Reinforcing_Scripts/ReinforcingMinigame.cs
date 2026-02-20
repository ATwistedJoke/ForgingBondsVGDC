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
    [SerializeField] private int minPresses = 5;
    [SerializeField] private int maxPresses2 = 15;
    [SerializeField] private float type1TimeLimit = 10f;

    [SerializeField] private float timeLimit2 = 3f;
    
    [Header("Added third minigame")]
    //
    [SerializeField] private float minTime = 2.5f;
    [SerializeField] private float maxTime = 4.5f;
    [SerializeField] private float mercy = 0.3f; 
    [SerializeField] private float pressure = 8f;
    
    // call back to gamemanager
    public Action<int> OnMinigameComplete;
    
    // current state
    public float currentTime;
    public int qualityScore = 0;
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
    
    // hold + release state
    private bool type3IsHolding = false;
    private float type3HoldStartTime = 0f;
    private float type3CurrentHoldDuration = 0f;
    private bool type3HasReleased = false;
    private float targetHoldTime = 0f; //later set
    
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

    

    //rand
    private void StartRandomChallenge()
    {
        currentChallengeType = UnityEngine.Random.Range(1, 4);
        
        isChallengeActive = true;
        challengePanel.SetActive(true);
        
        if (currentChallengeType == 1)
        {
            StartType1Challenge();
        }
        else if (currentChallengeType == 2)
        {
            StartType2Challenge();
        }
        else
        {
            StartType3Challenge();
        }
    }
    
    //hold and release
    private void StartType1Challenge()
    {
        challengeTimer = type1TimeLimit;
        type1SuccessfulPresses = 0;
        challengeInstructionText.text = $"Press SPACE when the color changes\n{type1SuccessfulPresses}/{type1RequiredPresses} successful presses\nTime left: {challengeTimer:F1}s";
        StartCoroutine(Type1ProgressAnimation());
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
        challengeInstructionText.text = $"Press SPACE when the color changes \n{type1SuccessfulPresses}/{type1RequiredPresses} successful presses\n\nOnly {challengeTimer:F1}s left";
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
        type2RequiredPresses = UnityEngine.Random.Range(minPresses, maxPresses2 + 1);
        type2PressCount = 0;
        challengeTimer = timeLimit2;
                
        challengeInstructionText.text = $"MASH SPACEBAR!!!\n{type2PressCount}/{type2RequiredPresses} presses";
        
        if (challengeProgressBar != null)
        {
            challengeProgressBar.fillAmount = 0f;
            challengeProgressBar.color = Color.orange;
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
        challengeInstructionText.text = $"MASH SPACEBAR!!!\n{type2PressCount}/{type2RequiredPresses} presses\n\nOnly {challengeTimer:F1}s left";
        
        if (challengeProgressBar != null)
        {
            challengeProgressBar.fillAmount = (float)type2PressCount / type2RequiredPresses;
        }
    }
    


    //new part starts here
    private void StartType3Challenge()
    {
        type3IsHolding = false;
        type3HoldStartTime = 0f;
        type3CurrentHoldDuration = 0f;
        type3HasReleased = false;
        challengeTimer = pressure;

        targetHoldTime = UnityEngine.Random.Range(minTime, maxTime);

        challengeInstructionText.text = $"Hold SPACE for {targetHoldTime:F1}s\n\nOnly {challengeTimer:F1}s left";
        
        if (challengeProgressBar != null)
        {
            challengeProgressBar.fillAmount = 0f;
            challengeProgressBar.color = Color.yellow;
        }
        
        StartCoroutine(Type3TimerCountdown());
    }
    
    private IEnumerator Type3TimerCountdown()
    {
        while (isChallengeActive && currentChallengeType == 3 && challengeTimer > 0)
        {
            challengeTimer -= Time.deltaTime;
            UpdateGame3Text();
            yield return null;
        }


        if (challengeTimer <= 0 && isChallengeActive && currentChallengeType == 3)
        {
            FailChallenge();
        }
    }
    
    private void HandleType3Input()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame && !type3HasReleased)
        {
            type3IsHolding = true;
            type3HoldStartTime = Time.time;
        }
        
        if (type3IsHolding && Keyboard.current.spaceKey.isPressed)
        {
            type3CurrentHoldDuration = Time.time - type3HoldStartTime;
            UpdateGame3Text();
            
            if (challengeProgressBar != null)
            {
                float progress = Mathf.Clamp01(type3CurrentHoldDuration / targetHoldTime);
                challengeProgressBar.fillAmount = progress;
                
                float minTarget = targetHoldTime - mercy;
                float maxTarget = targetHoldTime + mercy;
                
                //color change part 
                if (type3CurrentHoldDuration >= minTarget && type3CurrentHoldDuration <= maxTarget)
                {
                    challengeProgressBar.color = Color.green;
                }
                else if (type3CurrentHoldDuration > maxTarget)
                {
                    challengeProgressBar.color = Color.blue;
                }
                else
                {
                    challengeProgressBar.color = Color.purple;
                }
            }
        }

        //track release
        if (Keyboard.current.spaceKey.wasReleasedThisFrame && type3IsHolding && !type3HasReleased)
        {
            type3IsHolding = false;
            type3HasReleased = true;
            
            //check for fail
            float minTarget = targetHoldTime - mercy;
            float maxTarget = targetHoldTime + mercy;
            
            if (type3CurrentHoldDuration >= minTarget && type3CurrentHoldDuration <= maxTarget)
            {
                CompleteChallenge(true);
            }
            else
            {
                FailChallenge();
            }
        }
    }
    
    private void UpdateGame3Text()
    {
        string holdText = type3IsHolding ? $"Holding: {type3CurrentHoldDuration:F2}s" : "Press SPACE to start";
        challengeInstructionText.text = $"Hold SPACE for exactly {targetHoldTime:F1}s\n{holdText}\n\nOnly {challengeTimer:F1}s left";
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
        else if (currentChallengeType == 3)
        {
            HandleType3Input();
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
    

    private int ResultCalculation(int qualityScore)
    {
        int result = 0; 
        if(qualityScore >= 60)
        {
            result++; 
        }
        if(qualityScore >= 80)
        {
            result++; 
        }
        return result; 
    }

    
    private void EndMinigame()
    {
        isMinigameActive = false;
        minigameCanvas.SetActive(false);
        
        OnMinigameComplete?.Invoke(qualityScore);
        GameManager.instance.GiveResult(ResultCalculation(qualityScore)); 
        //Destroy(gameObject);
        GameObject rem = GameObject.FindGameObjectWithTag("minigame");
        Destroy(rem);
    }
    
}