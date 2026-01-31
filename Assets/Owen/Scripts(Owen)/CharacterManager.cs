using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    [SerializeField] public SpriteRenderer renderer; 
    [SerializeField] public Sprite[] list;
    public Rigidbody2D rb; 
    
    public int currSp; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currSp = 0; 
    }

    // Update is called once per frame
    void Update()
    {
        renderer.sprite = list[currSp]; 
    }

}
