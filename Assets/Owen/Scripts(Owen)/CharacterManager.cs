using System.Numerics;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    [SerializeField] public SpriteRenderer rnd; 
    [SerializeField] public Sprite[] list; 
    
    public int currSp; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rnd = GetComponent<SpriteRenderer>(); 
        currSp = 0; 
    }

    // Update is called once per frame
    void Update()
    {
        rnd.sprite = list[currSp]; 
    }

    public void ChangeSprite(int i)
    {
        Debug.Log("Called");
        currSp = i; 
    }

}
