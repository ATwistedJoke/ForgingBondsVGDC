using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;

[System.Serializable]
public struct PaintStage
{
    public string stageName;       
    public Sprite paintableSprite; // NEW: The visual grey outline they actually look at
    public Sprite scoreMapSprite;  // The hidden green/red answer key for math
    public Sprite prizeSprite;     // The colored image you steal the color from
    public Color targetColor;      
    public float totalTargets;     
}

public class ScoreManager : MonoBehaviour
{
    [Header("1. Assign These")]
    public SpriteRenderer paintableRenderer; // NEW: Drag your 'greyShield' object here!
    public SpriteRenderer scoreMapRenderer;  // Drag your 'ScoreMap' object here!
    public TextMeshProUGUI liveScoreText;   
    public TextMeshProUGUI timerText;
    public GameObject rulesPopupPanel; 
    
    [Tooltip("Drag your PixelPainter script here")]
    public PixelPainter pixelPainter; 
    
    [Tooltip("Create an Empty GameObject to hold the paint, and drag it here!")]
    public Transform paintContainer; 

    [Header("2. Stages Setup")]
    public PaintStage[] stages;
    private int currentStageIndex = 0;

    [Header("3. Game Settings")]
    public float timeLimit = 60f; 
    public float closeDelay = 5.0f; 
    public float flashStartTime = 10.0f; 
    [Range(0f, 1f)] public float colorTolerance = 0.2f;
    [Range(0f, 5f)] public float penaltyMultiplier = 1.0f; 

    [Header("4. Debug Stats")]
    public bool gameIsActive = false; 
    public bool isGameOver = false; 
    public float goodHits = 0;
    public float badHits = 0;
    
    private float currentDisplayAccuracy = 0f; 
    private HashSet<Vector2Int> visitedPixels = new HashSet<Vector2Int>();
    private Texture2D scoreTexture;
    private float timeRemaining;
    
    [Header("Cursor Settings")]
    public Texture2D brushCursor; 
    public Vector2 hotspot = new Vector2(0, 0);

    private Vector2 lastProcessedPos; 

    //Score Chekcing
    public float scoreCheck = 0;

    void Start()
    {
        timeRemaining = timeLimit; 
        gameIsActive = false;
        isGameOver = false; 
        
        if (rulesPopupPanel != null) rulesPopupPanel.SetActive(true);

        if (stages.Length > 0)
        {
            SetupStage(0);
        }
    }

    public void StartGameButtonHit()
    {
        if (rulesPopupPanel != null) rulesPopupPanel.SetActive(false);

        Cursor.SetCursor(brushCursor, hotspot, CursorMode.Auto);
        Cursor.visible = true; 
        
        gameIsActive = true;
        Debug.Log("Game Started!");
    }

    void Update()
    {
        if (isGameOver) 
        {
            if (liveScoreText != null)
            {
                float flash = Mathf.PingPong(Time.unscaledTime * 10, 1);
                string fullSentence = $"Accuracy: {currentDisplayAccuracy:F1}%";
                
                if (flash > 0.5f) liveScoreText.text = $"<color=green>{fullSentence}</color>";
                else liveScoreText.text = $"<color=white>{fullSentence}</color>";
                
                liveScoreText.ForceMeshUpdate(); 
            }
            return; 
        }

        if (!gameIsActive) return;

        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
        }
        else
        {
            timeRemaining = 0;
            AdvanceStage(); 
        }

        if (timerText != null)
        {
            int minutes = Mathf.FloorToInt(timeRemaining / 60F);
            int seconds = Mathf.FloorToInt(timeRemaining % 60F);
            int milliseconds = Mathf.FloorToInt((timeRemaining * 100F) % 100F);
            timerText.text = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, milliseconds);
            
            if (timeRemaining <= flashStartTime)
            {
                if (Mathf.PingPong(Time.time * 5, 1) > 0.5f) timerText.color = Color.red;
                else timerText.color = Color.white;
            }
            else
            {
                timerText.color = Color.white;
            }
        }
    }

    void SetupStage(int index)
    {
        currentStageIndex = index;
        PaintStage currentStage = stages[index];

        // 1. Swap ALL THREE images instantly
        if (paintableRenderer != null) paintableRenderer.sprite = currentStage.paintableSprite;
        if (scoreMapRenderer != null) scoreMapRenderer.sprite = currentStage.scoreMapSprite;
        
        if (pixelPainter != null && pixelPainter.prizeLayer != null)
        {
            pixelPainter.prizeLayer.sprite = currentStage.prizeSprite;
        }

        // 2. Update the texture we read the math from
        scoreTexture = scoreMapRenderer.sprite.texture;

        // 3. Reset scores
        goodHits = 0;
        badHits = 0;
        currentDisplayAccuracy = 0f;
        visitedPixels.Clear();

        if (liveScoreText != null) liveScoreText.text = $"Accuracy: 0.0%";
	timeRemaining = timeLimit;
	
        Debug.Log($"Starting Stage: {currentStage.stageName}");
    }

    void AdvanceStage()
    {
        Debug.Log("Stage Complete!");

        if (paintContainer != null)
        {
            foreach (Transform child in paintContainer)
            {
                Destroy(child.gameObject);
            }
        }

        if (currentStageIndex + 1 >= stages.Length)
        {
            EndGame();
        }
        else
        {
            SetupStage(currentStageIndex + 1); 
        }
    }

    void EndGame()
    {
        if (isGameOver) return; 

        Debug.Log("🏁 GAME OVER!");
        gameIsActive = false; 
        isGameOver = true; 
        
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        Destroy(transform.root.gameObject, closeDelay); 
        GameObject rem = GameObject.FindGameObjectWithTag("minigame");
        Destroy(rem);
    }

    bool IsMatch(Color c)
    {
        if (stages.Length == 0) return false;
        
        Color target = stages[currentStageIndex].targetColor;

        float diff = Mathf.Abs(c.r - target.r) + 
                     Mathf.Abs(c.g - target.g) + 
                     Mathf.Abs(c.b - target.b);
        return diff < colorTolerance;
    }

    public void CheckPixelAt(Vector2 worldPos)
    {
        if (!gameIsActive) return; 
        if (Vector2.Distance(worldPos, lastProcessedPos) < 0.05f) return;
        lastProcessedPos = worldPos;

        Vector3 localPos = scoreMapRenderer.transform.InverseTransformPoint(worldPos);
        float textureX = (localPos.x * scoreMapRenderer.sprite.pixelsPerUnit) + (scoreTexture.width / 2);
        float textureY = (localPos.y * scoreMapRenderer.sprite.pixelsPerUnit) + (scoreTexture.height / 2);
        Vector2Int pixelCoord = new Vector2Int(Mathf.RoundToInt(textureX), Mathf.RoundToInt(textureY));

        if (pixelCoord.x < 0 || pixelCoord.x >= scoreTexture.width || pixelCoord.y < 0 || pixelCoord.y >= scoreTexture.height) return;
        if (visitedPixels.Contains(pixelCoord)) return;

        Color c = scoreTexture.GetPixel(pixelCoord.x, pixelCoord.y);
        if (c.a < 0.1f) return;

	Debug.Log($"I see Color: {c}. I am looking for: {stages[currentStageIndex].targetColor}");

        if (IsMatch(c)) 
        {
            goodHits++;
            visitedPixels.Add(pixelCoord);
        }
        else 
        {
            badHits++;
            visitedPixels.Add(pixelCoord);
        }

        CalculateScore();
    }

    void CalculateScore()
    {
        if (stages.Length == 0) return;
        
        float total = stages[currentStageIndex].totalTargets;
        if (total == 0) return;

        //float currentScore = goodHits - (badHits * penaltyMultiplier);
        scoreCheck = goodHits - (badHits * penaltyMultiplier);
        float percent = (/*currentScore*/ scoreCheck / total) * 100f;
        
        currentDisplayAccuracy = Mathf.Clamp(percent, 0f, 100f);

        if (gameIsActive && liveScoreText != null)
        {
            liveScoreText.text = $"Accuracy: {currentDisplayAccuracy:F1}%";
        }

        if (currentDisplayAccuracy >= 100f && gameIsActive)
        {
            AdvanceStage();
        }
    }

    private void OnDestroy()
    {
        int result = 0; 
        float accuracy = scoreCheck * 100f;
        if(accuracy >= 90)
        {
            result = 2; 
        }
        else if(accuracy >= 70)
        {
            result = 1; 
        }
        GameManager.instance.GiveResult(result); 
    }
}