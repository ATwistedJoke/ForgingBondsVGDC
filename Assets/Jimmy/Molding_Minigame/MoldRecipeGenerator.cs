using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MoldRecipeGenerator : MonoBehaviour {
    public Molding_Minigame gameManager;

    //[Header("UI Text Fields")]
    // public TextMeshProUGUI ironText;
    // public TextMeshProUGUI goldText;
    // public TextMeshProUGUI copperText;

    // This stores the current target recipe
    public Dictionary<Molding_Minigame.OreType, int> currentRecipe = new Dictionary<Molding_Minigame.OreType, int>();

    public static OreRecipeGenerator instance;

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
        //reset recipe
        currentRecipe.Clear();

        //generate random recipe
        int ore1Target = Random.Range(1, 6);
        int ore2Target = Random.Range(1, 4);
        int ore3Target = Random.Range(1, 3);

        currentRecipe[Molding_Minigame.OreType.Iron] = ore1Target;
        currentRecipe[Molding_Minigame.OreType.Gold] = ore2Target;
        currentRecipe[Molding_Minigame.OreType.Mythril] = ore3Target;

        Debug.Log($"New Recipe: Iron {ore1Target}, Gold {ore2Target}, Copper {ore3Target}");
    }
}
