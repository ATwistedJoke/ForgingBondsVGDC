using UnityEngine;
using System.Collections;

public class Molding_Minigame : MonoBehaviour
{
    public enum WeaponType { Arrow, Morningstar}
    public WeaponType currentWeaponChoice; 
    public enum OreType { Iron, Gold, Mythril }
    public int total_molds = 0;

    public bool correct_mold = false;
    public bool spawning = false;
    public int totalOreCount;


    public Mold arrow_mold;
    public Mold morningstar_mold;
    public Crucible crucible;
    public GameObject game_container;    
    public RecipeGenerator recipeGenerator;
    public Stream stream;

    public float spawn_interval = 1.5f;


    private Mold current_mold;
    void Start_Minigame(WeaponType type)
    {   
        game_container.SetActive(true);
        currentWeaponChoice = type;
        
    SpawnNewMold();
        
    }

    public void Start(){

        Start_Minigame(currentWeaponChoice);

    }

    public void End_Minigame()
    {
        StopAllCoroutines();

        game_container.SetActive(false);
        GameManager.instance.GiveResult(CalculateResult(total_molds));
        GameObject rem = GameObject.FindGameObjectWithTag("minigame");
        Destroy(rem); 
    }

    //compares current crucible contents with generated recipe ticket
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
        spawning = true; 

        Debug.Log("mold is filled");
        if (filledMold != null && CheckRecipe())
        {
            total_molds++;
            Debug.Log("correct recipe, total molds incremented");
        }
        crucible.ClearContents();
        recipeGenerator.GenerateNewRecipe();

        if(filledMold != null)
        {
            Destroy(filledMold.gameObject);
        }

        yield return new WaitForSeconds (spawn_interval);
        SpawnNewMold();

    }
    

    public void SpawnNewMold()
    {
        Debug.Log("mold shold print");
        Mold prefabToSpawn;
        
        if(currentWeaponChoice == WeaponType.Arrow)
        {
            prefabToSpawn = arrow_mold;
        }
        else
        {
            prefabToSpawn = morningstar_mold;
        }
        if (prefabToSpawn != null)
        {
            Mold new_mold = Instantiate(prefabToSpawn, game_container.transform);
            current_mold = new_mold.GetComponent<Mold>();
            current_mold.game = this;
        }

    }
    private int CalculateResult(int input)
    {
        int result = 0; 
        if(input >= 4)
        {
            result++; 
            if(input >= 8)
            {
                result++; 
            }
        }
        return result; 
    }
}