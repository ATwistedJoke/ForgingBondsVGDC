using UnityEngine;
using System.Collections.Generic;

public class ScoreManager : MonoBehaviour
{
    [Header("The Answer Key")]
    public SpriteRenderer scoreMapRenderer; // Assign your Green/Red image here
    
    // We use a HashSet to make sure we don't count the same pixel twice
    private HashSet<Vector2Int> visitedPixels = new HashSet<Vector2Int>();
    
    private Texture2D scoreTexture;
    private float totalGoodPixels = 0;
    private float revealedGoodPixels = 0;
    private float revealedBadPixels = 0;

    void Start()
    {
        //test stuff
	Debug.Log("ScoreManager: I am AWAKE and ready."); // Proof script is running
        
        if (scoreMapRenderer == null)
        {
            Debug.LogError("CRITICAL: You forgot to assign the ScoreMap Image in the Inspector!");
            return;
        }
	
	//finish test stuff
	
	// 1. Get the texture so we can read it
        scoreTexture = scoreMapRenderer.sprite.texture;

        // 2. Hide the map (we don't need to see it, just read it)
        scoreMapRenderer.enabled = false; 

        // 3. Count how many Green pixels exist in total (This is our "100%")
        CalculateTotalPossibleScore();
    }

    void CalculateTotalPossibleScore()
    {
        Color[] pixels = scoreTexture.GetPixels();
        foreach (Color p in pixels)
        {
            // If pixel is very Green
            if (p.g > 0.8f && p.r < 0.2f) 
            {
                totalGoodPixels++;
            }
        }
        Debug.Log($"Game Started! Total Good Pixels to find: {totalGoodPixels}");
    }

    public void CheckPixelAt(Vector2 worldPos)
    {
        // 1. Convert World Position to Pixel Coordinates
        Vector3 localPos = scoreMapRenderer.transform.InverseTransformPoint(worldPos);
        float textureX = (localPos.x * scoreMapRenderer.sprite.pixelsPerUnit) + (scoreTexture.width / 2);
        float textureY = (localPos.y * scoreMapRenderer.sprite.pixelsPerUnit) + (scoreTexture.height / 2);

        Vector2Int pixelCoord = new Vector2Int(Mathf.RoundToInt(textureX), Mathf.RoundToInt(textureY));

        // 2. Safety Check: Are we even on the image?
        if (pixelCoord.x < 0 || pixelCoord.x >= scoreTexture.width || pixelCoord.y < 0 || pixelCoord.y >= scoreTexture.height) 
        {
            // Debug.Log("Missed the image entirely!"); 
            return;
        }

        // 3. Duplicate Check: Don't count the same pixel twice!
        if (visitedPixels.Contains(pixelCoord)) return;

        // 4. Read the color
        Color c = scoreTexture.GetPixel(pixelCoord.x, pixelCoord.y);

        // --- THE SCORING LOGIC ---
        
        // Check for GREEN (Good)
        if (c.g > 0.5f && c.r < 0.5f) 
        {
            revealedGoodPixels++;
            visitedPixels.Add(pixelCoord);
            
            // CALCULATE AND PRINT SCORE IMMEDIATELY
            float currentScore = GetCurrentAccuracy();
            Debug.Log($"✅ HIT GREEN! Current Accuracy: {currentScore:F2}%");
        }
        // Check for RED (Bad)
        else if (c.r > 0.5f && c.g < 0.5f)
        {
            revealedBadPixels++;
            visitedPixels.Add(pixelCoord);
            
            float currentScore = GetCurrentAccuracy();
            Debug.Log($"❌ HIT RED! Ouch. Score dropped to: {currentScore:F2}%");
        }
        else
        {
            // If we hit transparent or empty space, tell us why
             // Debug.Log($"Hit nothing (Color: {c})");
        }
    }


    public float GetCurrentAccuracy()
    {
        if (totalGoodPixels == 0) return 0;

        float percentRevealed = (revealedGoodPixels / totalGoodPixels) * 100f;
        float penalty = (revealedBadPixels / totalGoodPixels) * 100f; // Penalize based on size of object

        return Mathf.Max(0, percentRevealed - penalty);
    }
}
