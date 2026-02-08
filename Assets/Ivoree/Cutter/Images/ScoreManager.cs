using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [Header("1. Assign These")]
    public SpriteRenderer scoreMapRenderer; 
    public TextMeshProUGUI liveScoreText;   

    [Header("2. Scoring Rules")]
    [Tooltip("Click the Eyedropper, then click the Good (Green/Yellow) part.")]
    public Color targetColor = Color.green; 
    
    [Range(0f, 1f)] public float colorTolerance = 0.2f;

    [Header("3. Penalty Settings")]
    [Tooltip("How much score do you lose for hitting the wrong color?")]
    [Range(0f, 5f)] public float penaltyMultiplier = 1.0f; 

    [Header("4. Debug Stats")]
    public float totalTargets = 1500; // Default hardcoded value
    public float goodHits = 0;
    public float badHits = 0;
    
    private HashSet<Vector2Int> visitedPixels = new HashSet<Vector2Int>();
    private Texture2D scoreTexture;

    void Start()
    {
        scoreTexture = scoreMapRenderer.sprite.texture;
        RecalculateTotal(); // Sets our hardcoded total
    }

    [ContextMenu("Recalculate Total Score")]
    public void RecalculateTotal()
    {
        // HARDCODED TOTAL (The "1500" Fix)
        totalTargets = 2000; 
        
        // Reset current score
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
        Vector3 localPos = scoreMapRenderer.transform.InverseTransformPoint(worldPos);
        float textureX = (localPos.x * scoreMapRenderer.sprite.pixelsPerUnit) + (scoreTexture.width / 2);
        float textureY = (localPos.y * scoreMapRenderer.sprite.pixelsPerUnit) + (scoreTexture.height / 2);
        Vector2Int pixelCoord = new Vector2Int(Mathf.RoundToInt(textureX), Mathf.RoundToInt(textureY));

        if (pixelCoord.x < 0 || pixelCoord.x >= scoreTexture.width || pixelCoord.y < 0 || pixelCoord.y >= scoreTexture.height) return;
        
        // STOP if we already counted this pixel (Good OR Bad)
        if (visitedPixels.Contains(pixelCoord)) return;

        Color c = scoreTexture.GetPixel(pixelCoord.x, pixelCoord.y);

        // IGNORE INVISIBLE PIXELS (The "Mouse Check" you just did)
        if (c.a < 0.1f) return;

        // SCORING LOGIC
        if (IsMatch(c)) 
        {
            // GOOD HIT
            goodHits++;
            visitedPixels.Add(pixelCoord);
            // Debug.Log("✅ Good!"); 
        }
        else 
        {
            // BAD HIT (It's visible, but not the right color)
            badHits++;
            visitedPixels.Add(pixelCoord);
            // Debug.Log("❌ Bad!");
        }

        UpdateScoreUI();
    }

    void UpdateScoreUI()
    {
        if (totalTargets == 0) return;

        // THE FORMULA: Score = Good - (Bad * Penalty)
        float currentScore = goodHits - (badHits * penaltyMultiplier);
        
        // Convert to Percentage
        float percent = (currentScore / totalTargets) * 100f;
        
        // Clamp it (Cannot go below 0% or above 100%)
        float finalAccuracy = Mathf.Clamp(percent, 0f, 100f);

        if (liveScoreText != null)
            liveScoreText.text = $"Accuracy: {finalAccuracy:F1}%";
    }
}