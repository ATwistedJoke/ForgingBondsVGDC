using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private int score; 
    [SerializeField]private GameObject iron_ore; 
    [SerializeField]private GameObject gold_ore; 
    [SerializeField] private GameObject mythril_ore; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    //void Start()
    //{
      //  StartCoroutine(SpawnRandomOre());
        
    //}
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
    
    //spawn ore randomly around the screen
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
