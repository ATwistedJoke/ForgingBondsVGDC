using UnityEngine;

public class MouseManager : MonoBehaviour
{
    [SerializeField] private Texture2D pickaxetexture;
    [SerializeField] private Texture2D pickaxe_on_ore;
    public bool cursor_switch;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        SetDefault();
    }

    //basic pickaxe sprite
    public void SetDefault()
    {
        Vector2 hotspot = new Vector2(pickaxetexture.width / 2, pickaxetexture.height / 2);

        Cursor.SetCursor(pickaxetexture, hotspot, CursorMode.ForceSoftware);

    }   

    public void ResetMouse()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    
    // Ensure the cursor is actually visible if you hid it
        Cursor.visible = true;
    }

    //changes sprite to mining variant, optionally will add a rotation animation
    public void ChangeCursor()
    {
        if(pickaxe_on_ore != null)
        {
            Vector2 hotspot = new Vector2(pickaxe_on_ore.width / 2, pickaxe_on_ore.height / 2);
            Cursor.SetCursor(pickaxe_on_ore, hotspot, CursorMode.ForceSoftware); 
            Debug.Log("sprite changed");
        }
    }
    
}

