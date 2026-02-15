using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [Header("1. Assign These")]
    public SpriteRenderer scoreMapRenderer; 
    public TextMeshProUGUI liveScoreText;   
    public TextMeshProUGUI timerText;
    public GameObject rulesPopupPanel; 

    [Header("2. Game Settings")]
    public float timeLimit = 60f; 
    public float closeDelay = 5.0f; 
    
    [Tooltip("At how many seconds should the timer start flashing red?")]
    public float flashStartTime = 10.0f; 
    
    [Tooltip("Click Eyedropper -> Click Green part")]
    public Color targetColor = Color.green; 
    [Range(0f, 1f)] public float colorTolerance = 0.2f;
    [Range(0f, 5f)] public float penaltyMultiplier = 1.0f; 

    [Header("3. Debug Stats")]
    public bool gameIsActive = false; 
    public bool isGameOver = false; // <--- NEW FLAG
    public float totalTargets = 1500; 
    public float goodHits = 0;
    public float badHits = 0;
    
    private float currentDisplayAccuracy = 0f; 
    
    private HashSet<Vector2Int> visitedPixels = new HashSet<Vector2Int>();
    private Texture2D scoreTexture;
    private float timeRemaining;

    void Start()
    {
        scoreTexture = scoreMapRenderer.sprite.texture;
        
	Debug.Log("Corner Alpha: " + scoreTexture.GetPixel(0,0).a);
	
	timeRemaining = timeLimit; 
        
        // Start Paused (Rules Screen)
        gameIsActive = false;
        isGameOver = false; 
        
        if (rulesPopupPanel != null) rulesPopupPanel.SetActive(true);

        RecalculateTotal(); 
    }

    public void StartGameButtonHit()
    {
        if (rulesPopupPanel != null) rulesPopupPanel.SetActive(false);
        gameIsActive = true;
        Debug.Log("Game Started!");
    }

    void Update()
    {
        // STATE 1: GAME OVER (Flashing Lights)
        // ================================================================
        // Only flash if the game is actually OVER (not just paused at start)

        if (isGameOver) 
        {
            if (liveScoreText != null)
            {
                float flash = Mathf.PingPong(Time.unscaledTime * 10, 1);
                string fullSentence = $"Accuracy: {currentDisplayAccuracy:F1}%";
                
                if (flash > 0.5f) 
                    liveScoreText.text = $"<color=green>{fullSentence}</color>";
                else 
                    liveScoreText.text = $"<color=white>{fullSentence}</color>";
                
                liveScoreText.ForceMeshUpdate(); 
            }
            return; // Stop here, don't run timer logic
        }

        // STATE 2: PAUSED (Rules Screen)
        // ================================================================
        if (!gameIsActive) return;

        // STATE 3: GAME RUNNING
        // ================================================================

        // A. Timer Logic
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
        }
        else
        {
            timeRemaining = 0;
            EndGame(); 
        }

        // B. Update Timer UI
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

    void EndGame()
    {
        if (isGameOver) return; // Don't run twice

        Debug.Log("🏁 GAME OVER! Flashing lights starting...");
        gameIsActive = false; 
        isGameOver = true; // <--- This triggers the top block in Update()
        
        Destroy(transform.root.gameObject, closeDelay); 
    }

    [ContextMenu("Recalculate Total Score")]
    public void RecalculateTotal()
    {
        totalTargets = 1500; 
        goodHits = 0;
        badHits = 0;
        visitedPixels.Clear();
        CalculateScore(); 
    }

    bool IsMatch(Color c)
    {
        float diff = Mathf.Abs(c.r - targetColor.r) + 
                     Mathf.Abs(c.g - targetColor.g) + 
                     Mathf.Abs(c.b - targetColor.b);
        return diff < colorTolerance;
    }

    public void CheckPixelAt(Vector2 worldPos)
    {
        // Prevent painting if game hasn't started OR is already over
        if (!gameIsActive) return; 

        Vector3 localPos = scoreMapRenderer.transform.InverseTransformPoint(worldPos);
        float textureX = (localPos.x * scoreMapRenderer.sprite.pixelsPerUnit) + (scoreTexture.width / 2);
        float textureY = (localPos.y * scoreMapRenderer.sprite.pixelsPerUnit) + (scoreTexture.height / 2);
        Vector2Int pixelCoord = new Vector2Int(Mathf.RoundToInt(textureX), Mathf.RoundToInt(textureY));

        if (pixelCoord.x < 0 || pixelCoord.x >= scoreTexture.width || pixelCoord.y < 0 || pixelCoord.y >= scoreTexture.height) return;
        if (visitedPixels.Contains(pixelCoord)) return;

        Color c = scoreTexture.GetPixel(pixelCoord.x, pixelCoord.y);

        if (c.a < 0.1f) return;

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
        if (totalTargets == 0) return;
        float currentScore = goodHits - (badHits * penaltyMultiplier);
        float percent = (currentScore / totalTargets) * 100f;
        
        currentDisplayAccuracy = Mathf.Clamp(percent, 0f, 100f);

        if (gameIsActive && liveScoreText != null)
        {
            liveScoreText.text = $"Accuracy: {currentDisplayAccuracy:F1}%";
        }

        // STILL WIN AT 100%
        if (currentDisplayAccuracy >= 100f && gameIsActive)
        {
            EndGame();
        }
    }
}