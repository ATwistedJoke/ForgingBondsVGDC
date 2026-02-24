using UnityEngine;
using UnityEngine.InputSystem; 

public class PixelPainter : MonoBehaviour
{
    [Header("Brush Settings")]
    public GameObject paintBlobPrefab; 
    public float pixelSpacing = 0.05f; 
    
    [Header("Color Sampling")]
    public SpriteRenderer prizeLayer; 

    [Header("References")]
    public ScoreManager scoreManager;
    public bool canPaint = true;

    private Vector3 lastPaintPosition;
    private Camera mainCam;

    void Start()
    {
        mainCam = Camera.main; 
    }

    void Update()
    {
        // WIRETAP 1: Is the game actually active?
        if (!canPaint || scoreManager == null || !scoreManager.gameIsActive) 
        {
            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                Debug.LogWarning("PAINT BLOCKED: Game is not active, or ScoreManager is missing!");
            }
            return;
        }

        if (Mouse.current == null) return;

        Vector2 mousePixels = Mouse.current.position.ReadValue();
        Vector3 worldPos = mainCam.ScreenToWorldPoint(new Vector3(mousePixels.x, mousePixels.y, Mathf.Abs(mainCam.transform.position.z)));
        worldPos.z = 0f; 

        if (Mouse.current.leftButton.isPressed)
        {
            float dist = Vector3.Distance(worldPos, lastPaintPosition);
            
            // WIRETAP 2: Are we moving the mouse enough?
            if (dist >= pixelSpacing)
            {
                ApplyPaint(worldPos); 
            }
            else if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                Debug.LogWarning($"PAINT BLOCKED: Mouse hasn't moved far enough. Distance: {dist}");
            }
        }
        else
        {
            lastPaintPosition = worldPos; 
        }
    }

    void ApplyPaint(Vector3 paintPos)
    {
        float x = Mathf.Round(paintPos.x / pixelSpacing) * pixelSpacing;
        float y = Mathf.Round(paintPos.y / pixelSpacing) * pixelSpacing;
        Vector3 snapPos = new Vector3(x, y, 0);

        // Spawn the Paint Blob INSIDE the container so it can be cleaned up!
        Transform container = scoreManager.paintContainer != null ? scoreManager.paintContainer : null;
        GameObject newBlob = Instantiate(paintBlobPrefab, snapPos, Quaternion.identity, container);

        if (prizeLayer != null && prizeLayer.sprite != null)
        {
            Color stolenColor = GetColorFromSprite(prizeLayer, snapPos);
            if (stolenColor.a > 0.1f) 
            {
                var main = newBlob.GetComponent<ParticleSystem>().main;
                main.startColor = stolenColor; 
            }
        }

        if (scoreManager != null) scoreManager.CheckPixelAt(snapPos);

        lastPaintPosition = paintPos;
    }

    Color GetColorFromSprite(SpriteRenderer renderer, Vector3 worldPos)
    {
        Vector3 localPos = renderer.transform.InverseTransformPoint(worldPos);
        Texture2D tex = renderer.sprite.texture;
        
        float textureX = (localPos.x * renderer.sprite.pixelsPerUnit) + (tex.width / 2);
        float textureY = (localPos.y * renderer.sprite.pixelsPerUnit) + (tex.height / 2);

        if (textureX < 0 || textureX >= tex.width || textureY < 0 || textureY >= tex.height) return Color.white;

        return tex.GetPixel(Mathf.RoundToInt(textureX), Mathf.RoundToInt(textureY));
    }
}