using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [Header("1. Assign These")]
    public SpriteRenderer scoreMapRenderer; 
    public TextMeshProUGUI liveScoreText;   
    public TextMeshProUGUI timerText; // <--- NEW SLOT! Drag 'TimerText' here

    [Header("2. Game Settings")]
    public float timeLimit = 30f; // Seconds to complete the level
    [Tooltip("Click Eyedropper -> Click Green part")]
    public Color targetColor = Color.green; 
    [Range(0f, 1f)] public float colorTolerance = 0.2f;
    [Range(0f, 5f)] public float penaltyMultiplier = 1.0f; 

    [Header("3. Debug Stats")]
    public bool gameIsActive = true; // Stops the game when time runs out
    public float totalTargets = 1500; 
    public float goodHits = 0;
    public float badHits = 0;
    
    private HashSet<Vector2Int> visitedPixels = new HashSet<Vector2Int>();
    private Texture2D scoreTexture;
    private float timeRemaining;

    void Start()
    {
        scoreTexture = scoreMapRenderer.sprite.texture;
        timeRemaining = timeLimit; // Start the clock
        gameIsActive = true;
        
        RecalculateTotal(); 
    }

    void Update()
    {
        if (!gameIsActive) return; // Stop updates if game is over

        // 1. COUNTDOWN LOGIC
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
        }
        else
        {
            // TIME'S UP!
            timeRemaining = 0;
            gameIsActive = false; 
            Debug.Log("⏰ TIME IS UP!");
        }

        // 2. UPDATE TIMER UI (Format: 00:00)
        if (timerText != null)
        {
            int minutes = Mathf.FloorToInt(timeRemaining / 60F);
            int seconds = Mathf.FloorToInt(timeRemaining % 60F);
            int milliseconds = Mathf.FloorToInt((timeRemaining * 100F) % 100F);
            timerText.text = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, milliseconds);
            
            // Optional: Turn red when low on time
            if (timeRemaining < 5) timerText.color = Color.red;
            else timerText.color = Color.white;
        }
    }

    [ContextMenu("Recalculate Total Score")]
    public void RecalculateTotal()
    {
        totalTargets = 1500; 
        goodHits = 0;
        badHits = 0;
        visitedPixels.Clear();
        UpdateScoreUI();
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
        // STOP if game is over (Time ran out)
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

        UpdateScoreUI();
    }

    void UpdateScoreUI()
    {
        if (totalTargets == 0) return;
        float currentScore = goodHits - (badHits * penaltyMultiplier);
        float percent = (currentScore / totalTargets) * 100f;
        float finalAccuracy = Mathf.Clamp(percent, 0f, 100f);

        if (liveScoreText != null)
            liveScoreText.text = $"Accuracy: {finalAccuracy:F1}%";
    }
}
