using UnityEngine;
using UnityEngine.InputSystem; // Needed for mouse check
using System.Collections.Generic;

public class PixelPainter : MonoBehaviour
{
	
    public ScoreManager scoreManager;
        


    [Header("Brush Settings")]
    public GameObject paintBlobPrefab; 
    public float pixelSpacing = 0.05f; // Keep this small for a smooth line

    private Vector3 lastPaintPosition;

    void Update()
    {
        // 1. Only paint if the Left Mouse Button is HELD DOWN
        if (Mouse.current.leftButton.isPressed)
        {
            float dist = Vector3.Distance(transform.position, lastPaintPosition);

            // 2. Spawn paint only if we moved enough
            if (dist >= pixelSpacing)
            {
                ApplyPaint();
            }
        }
        else
        {
            // Reset the "last position" when we let go so we can start a new line smoothly
            lastPaintPosition = transform.position; 
        }
    }

    void ApplyPaint()
    {
        // Snap to grid for clean pixel art look
        float x = Mathf.Round(transform.position.x / pixelSpacing) * pixelSpacing;
        float y = Mathf.Round(transform.position.y / pixelSpacing) * pixelSpacing;
        Vector3 snapPos = new Vector3(x, y, 0);

        Instantiate(paintBlobPrefab, snapPos, Quaternion.identity);
        lastPaintPosition = transform.position;



        //updated stuff 

	Instantiate(paintBlobPrefab, snapPos, Quaternion.identity);
	if (scoreManager == null)
        {
            Debug.LogError("CRITICAL: ScoreManager is MISSING! I cannot report the score.");
        }
        else
        {
            Debug.Log("Painter: Sending data to manager..."); // Proof we tried
            scoreManager.CheckPixelAt(snapPos);
        }

	lastPaintPosition = transform.position; 
    }
}