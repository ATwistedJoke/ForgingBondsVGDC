using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private int score; 
    [SerializeField]private GameObject iron_ore; 
    [SerializeField]private GameObject gold_ore; 
    [SerializeField] private GameObject mythril_ore; 

    public TextMeshPro textbox;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    StartCoroutine(SpawnRandomOre());
        
    }
    void Update()
    {
    {
    //left click down
    if (Input.GetMouseButtonDown(0))
    {   
        //get mouse position comparative to camera position
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        //shoot raycast from mouse position
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

        //check for collisions and ore property
        if (hit.collider != null)
        {
            Ores ore = hit.collider.GetComponent<Ores>();
            if (ore != null)
            {
                ore.Handle_Click();
            }
        }
    }
    }

    textbox.text = GetScore().ToString();

    }
    //getter function for score
    public int GetScore()
    {
        return score;
    }

    //AddScore(), adds score
    public void AddScore(int amount)
    {
        score += amount;
    }



    //based on weights, randomly pick an ore
    //from least to most likely (mythril -> gold -> iron)
    GameObject ChooseOreByRarity()
    {
        float roll = Random.value;

        // 70% Iron, 25% Gold, 5% Mythril
        if (roll < 0.7f)
            return iron_ore;
        else if (roll < 0.95f)
            return gold_ore;
        else
            return mythril_ore;
    }
    
    //Spawn ores randomly around the screen
    IEnumerator SpawnRandomOre()
    {

        while (true)
        {

    //choose a random position
    float randomX = Random.value;
    float randomY = Random.value;

    GameObject ore_spawn = ChooseOreByRarity();

    //choose a random ore, in descending probabilty (iron -> gold -> mythril)
    //create a vector3, based on the camera's axis
    Vector3 spawnPosition = Camera.main.ViewportToWorldPoint(new Vector3(randomX, randomY, 1f));

    Instantiate(ore_spawn, spawnPosition, Quaternion.identity);

    yield return new WaitForSeconds(Random.value * 2f);
        }
    
    }
}
