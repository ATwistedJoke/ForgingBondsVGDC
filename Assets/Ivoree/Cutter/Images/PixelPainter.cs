using UnityEngine;
using UnityEngine.InputSystem; 

public class PixelPainter : MonoBehaviour
{
    [Header("Brush Settings")]
    public GameObject paintBlobPrefab; 
    public float pixelSpacing = 0.05f; 
    
    [Header("Color Sampling")]
    public SpriteRenderer prizeLayer; // Drag your 'finalShield' or 'highlightShield' here!

    [Header("References")]
    public ScoreManager scoreManager;
    public bool canPaint = false;

    private Vector3 lastPaintPosition;

    void Update()
    {
        if (!canPaint) return;
        if (Mouse.current == null) return;

        // Paint on Hold
        if (Mouse.current.leftButton.isPressed)
        {
            float dist = Vector3.Distance(transform.position, lastPaintPosition);
            if (dist >= pixelSpacing)
            {
                ApplyPaint();
            }
        }
        else
        {
            lastPaintPosition = transform.position; 
        }
    }

    void ApplyPaint()
    {
        // 1. Snap Position
        float x = Mathf.Round(transform.position.x / pixelSpacing) * pixelSpacing;
        float y = Mathf.Round(transform.position.y / pixelSpacing) * pixelSpacing;
        Vector3 snapPos = new Vector3(x, y, 0);

        // 2. Spawn the Paint Blob
        GameObject newBlob = Instantiate(paintBlobPrefab, snapPos, Quaternion.identity);

        // 3. COLOR MAGIC: Sample the color from the prize layer
        if (prizeLayer != null)
        {
            Color stolenColor = GetColorFromSprite(prizeLayer, snapPos);
            // If the color is transparent (alpha 0), keep it white/dusty
            if (stolenColor.a > 0.1f) 
            {
                var main = newBlob.GetComponent<ParticleSystem>().main;
                main.startColor = stolenColor; // Dye the particles!
            }
        }

        // 4. Report Score
        if (scoreManager != null) scoreManager.CheckPixelAt(snapPos);

        lastPaintPosition = transform.position;
    }

    // Helper to read the pixel color
    Color GetColorFromSprite(SpriteRenderer renderer, Vector3 worldPos)
    {
        Vector3 localPos = renderer.transform.InverseTransformPoint(worldPos);
        Texture2D tex = renderer.sprite.texture;
        
        // Convert to Texture Coordinates
        float textureX = (localPos.x * renderer.sprite.pixelsPerUnit) + (tex.width / 2);
        float textureY = (localPos.y * renderer.sprite.pixelsPerUnit) + (tex.height / 2);

        // Safety Check
        if (textureX < 0 || textureX >= tex.width || textureY < 0 || textureY >= tex.height) return Color.white;

        return tex.GetPixel(Mathf.RoundToInt(textureX), Mathf.RoundToInt(textureY));
    }
}