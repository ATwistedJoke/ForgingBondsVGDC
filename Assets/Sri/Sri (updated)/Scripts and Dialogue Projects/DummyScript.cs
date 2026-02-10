using UnityEngine;

public class DummyScript : MonoBehaviour
{
    [SerializeField] public SpriteRenderer renderer; 
    [SerializeField] public Sprite[] list; 
    public int x; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        renderer.sprite = list[x];
    }

    public void changeSprite(int x)
    {
        this.x = x; 
    }
}
