using UnityEditor.Callbacks;
using UnityEngine;

public class Ore_Fragment : MonoBehaviour
{

    public enum OreType { Iron, Gold, Mythril }

    public OreType ore; 
    
    public GameController game;

    public int iron_add_score = 10;
    public int gold_add_score = 25;
    public int myth_add_score = 50;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
         Rigidbody2D rb = GetComponent<Rigidbody2D>();
    }

//checks each individual tag,  possible, i know very messy and can be shortened
    private void OnCollisionEnter2D(Collision2D collision) {
 
        if (collision.gameObject.CompareTag("Boundary"))
        {
            Destroy(gameObject);  
            Debug.Log("im died"); 
        }

}
    //collector object detection
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    if (collision.gameObject.CompareTag("Bag")) {
            switch (ore)
        {
            case OreType.Iron:
                game.AddScore(iron_add_score);
                Debug.Log("+10 points to you!");
                break;

            case OreType.Gold:
                game.AddScore(gold_add_score);
                Debug.Log("+25 points to you!");
                break;

            case OreType.Mythril:
                game.AddScore(myth_add_score);
                Debug.Log("+50 points to you!");
                break;
        }
       Debug.Log("i am hitting this");
       Destroy(gameObject);
    }

    if (collision.gameObject.CompareTag("Boundary"))
        {
            Destroy(gameObject);   
        }

}
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Boundary"))
        {
            
            Destroy(gameObject);
            
        }
    }
}
