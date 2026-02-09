using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem.LowLevel;

public class Molding_Minigame : MonoBehaviour
{
    // public Stream stream;
    // public RectTransform tipping_point;
    public GameObject mold;
    public enum OreType { Iron, Gold, Mythril }
    public Crucible crucible;
    public int totalOreCount;
    public GameObject game_container;
    public RecipeGenerator recipeGenerator;
    public int total_molds = 0;

    public bool correct_mold = false;

    private Mold current_mold;
    void Start_Minigame()
    {   
        game_container.SetActive(true);
        
    }

    void End_Minigame()
    {
        

    }

    // public void AddPoint()
    // {
    //     if(mold.GetComponent<Moldfilled && correct_mold == true)
    //     {
    //         total_molds++;
    //     }

    // }
    public bool CheckRecipe()
    {

        var playerContents = crucible.contents;
        var targetRecipe = recipeGenerator.currentRecipe;

        foreach (var ore in targetRecipe)
        {
            if (playerContents[ore.Key] != ore.Value)
            {
                return false;
            }
        }


        return true;
    }

    public void OnMoldFilled()
    {
        Debug.Log("mold is filled");
        crucible.ClearContents();
        if (CheckRecipe())
        {
            total_molds++;
        }
        SpawnNewMold();
    }

    public void SpawnNewMold()
    {
        Debug.Log("mold shold print");
        RectTransform mold_rt = mold.GetComponent<RectTransform>();

        GameObject new_mold = Instantiate(mold, mold_rt);

        current_mold = new_mold.GetComponent<Mold>();

        current_mold.game = this;
        }
}