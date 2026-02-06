using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Molding_Minigame : MonoBehaviour
{
    public Stream stream;
    public RectTransform tipping_point;
    public GameObject crucible;
    public Mold mold;

    public enum OreType { Iron, Gold, Copper }
    public Dictionary<OreType, int> crucibleContents;
    public int totalOreCount;
    public GameObject game_container;

    public void Awake()
    {
    crucibleContents = new Dictionary<OreType, int>(){
    { OreType.Iron, 0 },
    { OreType.Gold, 0 },
    { OreType.Copper, 0 }
    };
    }

    public void AddOre(OreType ore)
    {
        totalOreCount++;

        // Update the dictionary count
        if (crucibleContents.ContainsKey(ore))
        {
            crucibleContents[ore]++;
        }
        else
        {
            crucibleContents[ore] = 1;
        }
    }
    void Start_Minigame()
    {   
        game_container.SetActive(true);
        
    }
}