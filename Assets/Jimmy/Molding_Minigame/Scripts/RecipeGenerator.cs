
using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class RecipeGenerator : MonoBehaviour
{
    public Molding_Minigame gameManager;

    // This stores the current target recipe
    public Dictionary<Molding_Minigame.OreType, int> currentRecipe = new Dictionary<Molding_Minigame.OreType, int>();

    public static RecipeGenerator instance;

    public TextMeshProUGUI textbox1;
    public TextMeshProUGUI textbox2;
    public TextMeshProUGUI textbox3;


    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        GenerateNewRecipe();
    }

    public void GenerateNewRecipe()
    {
        //Clear the old recipe
        currentRecipe.Clear();

        //Randomize counts (1 to 5 ores each)
        int ore_1 = Random.Range(1, 6);
        int ore_2 = Random.Range(1, 4); // Make gold/mythril rarer
        int ore_3 = Random.Range(1, 4);

        //Store in the Dictionary
        currentRecipe[Molding_Minigame.OreType.Iron] = ore_1;
        currentRecipe[Molding_Minigame.OreType.Gold] = ore_2;
        currentRecipe[Molding_Minigame.OreType.Mythril] = ore_3;

        //Update TextUI
        textbox1.text = "Iron: " + ore_1;
        textbox2.text = "Gold: " + ore_2;
        textbox3.text = "Mythril: " + ore_3;


        Debug.Log($"New Recipe: Iron {ore_1}, Gold {ore_2}, Mythril {ore_3}");
    }
}