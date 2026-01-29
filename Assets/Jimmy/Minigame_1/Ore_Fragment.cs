using UnityEditor.Callbacks;
using UnityEngine;

public class Ore_Fragment : MonoBehaviour
{

    public enum OreType { Iron, Gold, Mythril }

    public OreType ore; 
    
    public GameController game;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
         Rigidbody2D rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        
    }

    //automatically called in Unity

    //
    private void OnCollisionEnter2D(Collision2D collision) {
        //checks each individual tag,  possible, i know very messy and can be shortened
    if (collision.gameObject.CompareTag("Bag")) {
       game.AddScore(20);
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
