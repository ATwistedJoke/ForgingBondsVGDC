using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem.LowLevel;
using Unity.VisualScripting;

public class Molding_Minigame : MonoBehaviour
{
    public enum OreType { Iron, Gold, Mythril }
    public int total_molds = 0;

    public bool correct_mold = false;
    public bool spawning = false;
    public int totalOreCount;


    public Mold mold;
    public Crucible crucible;
    public GameObject game_container;    
    public RecipeGenerator recipeGenerator;
    public Stream stream;

    public float spawn_interval = 1.5f;


    private Mold current_mold;
    void Start_Minigame()
    {   
        game_container.SetActive(true);

        if(mold != null)
        {
            SpawnNewMold();

        }
        
    }

    void Start(){

        Start_Minigame();

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
    

    /* call when a mold is filled
        clears the current contents in the crucible
        check recipe using CheckRecipe()
        delay of 0.5 seconds
    */
    public IEnumerator OnMoldFilled(Mold filledMold)
    {
        // if (spawning)
        // {
        //     yield break;
        // }

        spawning = true; 

        Debug.Log("mold is filled");
        if (CheckRecipe())
        {
            total_molds++;
        }
        crucible.ClearContents();

        yield return new WaitForSeconds (spawn_interval);
        SpawnNewMold();
        Destroy(filledMold.gameObject);

        // spawning = false;
    }

    public void SpawnNewMold()
    {
        Debug.Log("mold shold print");

        if(mold != null)
        {
        Mold new_mold = Instantiate(mold, game_container.transform);
        Debug.Log("spawned at " + game_container.transform);

        current_mold = new_mold.GetComponent<Mold>();

        current_mold.game = this;
        }


        }

}