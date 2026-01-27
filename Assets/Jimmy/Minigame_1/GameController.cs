using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.WSA;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] private int score; 
    [SerializeField] private bool game_playing = true; 
    [SerializeField]private GameObject iron_ore; 
    [SerializeField]private GameObject gold_ore; 
    [SerializeField] private GameObject mythril_ore; 

    public TextMeshProUGUI textbox;

    public RectTransform game_screen; 

    public GameObject minigame_1_panel;

    //placeholder for testing of end of minigame
    public Button end_game;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    Start_Minigame_1();
        
    }

    public void Start_Minigame_1()
    {
        minigame_1_panel.SetActive(true);
        StartCoroutine(SpawnRandomOre());
    }

    public void End_Minigame()
    {
        minigame_1_panel.SetActive(false);
        game_playing = false;
    }


    void Update()
    {

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

        while (game_playing)
        {
    
    //get panel screen width/height
        float width = game_screen.rect.width;
        float height = game_screen.rect.height; 

    //choose a random position
        float randomX = Random.Range(-width/2, width/2);
        float randomY = Random.Range(-height/2, height / 2);

    GameObject ore_spawn = ChooseOreByRarity();

    //choose a random ore, in descending probabilty (iron -> gold -> mythril)
    //create a vector3, based on the camera's axis
    Vector3 spawnPosition = new Vector3(randomX, randomY, 0f);

    Debug.Log(spawnPosition);

    //creates a new_ore, parents it to the panel
    GameObject new_ore = Instantiate(ore_spawn, game_screen);

    //
    RectTransform rt = new_ore.GetComponent<RectTransform>();
    rt.localPosition = new Vector3(randomX, randomY, 0f);
    rt.localScale = Vector3.one;

    new_ore.transform.SetParent(game_screen, false);


    //random time till new ore is spawned
    yield return new WaitForSeconds(Random.value * 2f);
        }
    
    }

}
