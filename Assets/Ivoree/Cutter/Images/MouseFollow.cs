using UnityEngine;
using UnityEngine.InputSystem;

public class MouseFollower : MonoBehaviour
{
    private Camera mainCam;

    void Start()
    {
        mainCam = Camera.main;
        // Hide the system mouse cursor so you only see your brush
        Cursor.visible = false; 
    }

    void Update()
    {
        if (Mouse.current == null) return;

        // 1. Get raw mouse position
        Vector2 mousePixels = Mouse.current.position.ReadValue();
        
        // 2. Convert to World Point instantly
        Vector3 worldPos = mainCam.ScreenToWorldPoint(new Vector3(mousePixels.x, mousePixels.y, Mathf.Abs(mainCam.transform.position.z)));
        
        // 3. Lock Z to 0 (or whatever your player Z is)
        worldPos.z = transform.position.z;

        // 4. Teleport immediately
        transform.position = worldPos;
    }
}