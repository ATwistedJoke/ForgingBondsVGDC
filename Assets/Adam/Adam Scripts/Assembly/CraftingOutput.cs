using UnityEngine;
using System;

public class CraftingOutput : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Transform outputPos;
    public string[][] craftingRecipes = new string[10][];
    public GameObject[] curTable;
    public GameObject MorningstarHead;
    public GameObject Morningstar;
    public GameObject Bow;
    public GameObject Crank;
    public GameObject Stirrup;
    public GameObject CrossbowBase;
    public GameObject Crossbow;
    public GameObject Shaft;
    public GameObject PleiotiumCylinder;
    public GameObject Handle;
    public DragObject dragObject;
    public GameObject minigameHeader;

    void Start()
    {
        minigameHeader = GameObject.FindGameObjectWithTag("minigame");
        outputPos = transform;
        craftingRecipes[0] = new string[]{
                "", "", "Pleiotium",
                "", "Pleiotium", "", 
                "Pommel", "", "", "Handle"
                            };
        craftingRecipes[1] = new string[]{
                "", "", "",
                "Pleiotium", "Pleiotium", "Pleiotium", 
                "", "", "", "Pleiotium Cylinder"
                            };
        craftingRecipes[2] = new string[]{
                "", "", "Ring",
                "", "Pleiotium Cylinder", "", 
                "Ring2", "", "", "Shaft"
                            };
        craftingRecipes[3] = new string[]{
                "Spike", "Spike", "Spike",
                "Spike", "Ball", "Spike", 
                "Spike", "Spike", "Spike", "Morningstar Head"
                            };
        craftingRecipes[4] = new string[]{
                "", "Chain", "Morningstar Head",
                "", "Shaft", "", 
                "Handle", "", "", "Morningstar"
                            };
        craftingRecipes[5] = new string[]{
                "", "Pleiotium", "",
                "Pleiotium", "", "Pleiotium", 
                "String", "String", "String", "Bow"
                            };
        craftingRecipes[6] = new string[]{
                "", "Wood", "Wood",
                "Steel", "Pleiotium", "Steel", 
                "Steel", "Steel", "Steel", "Crank"
                            };
        craftingRecipes[7] = new string[]{
                "Steel", "Steel", "Steel",
                "Steel", "", "Steel", 
                "", "Steel", "", "Stirrup"
                            };
        craftingRecipes[8] = new string[]{
                "", "Pleiotium", "",
                "Wood", "Wood", "Wood", 
                "", "", "Wood", "Crossbow Base"
                            };
        craftingRecipes[9] = new string[]{
                "", "", "Crank",
                "Stirrup", "Bow", "Crossbow Base", 
                "", "", "", "Crossbow"
                            };
        
        curTable = new GameObject[9];
    }

    public void addItemToTable(GameObject item, int pos)
    {
        if(curTable[pos] != null)
        {
            removeItemFromTable(pos);
        }
        curTable[pos] = item;
        compareRecipes();
    }
    public void removeItemFromTable(int pos)
    {
        GameObject toRemove = curTable[pos];
        if(toRemove != null)
        {
            dragObject = toRemove.GetComponent<DragObject>();
            dragObject.returnToStartPos();
        }
        curTable[pos] = null;
    }
    private void compareRecipes()
    {
        foreach(string[] recipe in craftingRecipes)
        {
            if(compareArrays(recipe, curTable))
            {
                instantiateCraftedItem(recipe[9]);
                ClearGrid();
            }
        }
    }

    private bool compareArrays(string[] recipe, GameObject[] table)
    {
        for(int i = 0;i < 9; i++)
        {
            if(table[i] == null)
            {
                if(recipe[i] != "")
                {
                    return false;
                }
            }
            else {
                if(!recipe[i].Equals(table[i].tag))
                {
                    return false;
                }
            }
        }
        return true;
    }
    private void instantiateCraftedItem(string item)
    {
        if(item == null)
        {
            Debug.Log("null item tried to be created :/");
            return;
        }
        GameObject craftedObject = null;
        if (item.Equals("Handle"))
        {
            craftedObject = Instantiate(Handle, outputPos.position, Quaternion.identity);
        }
        if (item.Equals("Pleiotium Cylinder"))
        {
            craftedObject = Instantiate(PleiotiumCylinder, outputPos.position, Quaternion.identity);
        }
        if (item.Equals("Morningstar Head"))
        {
            craftedObject = Instantiate(MorningstarHead, outputPos.position, Quaternion.identity);
        }
        if (item.Equals("Shaft"))
        {
            craftedObject = Instantiate(Shaft, outputPos.position, Quaternion.identity);
        }
        if (item.Equals("Morningstar"))
        {
            Destroy(minigameHeader);
        }
        if (item.Equals("Bow"))
        {
            craftedObject = Instantiate(Bow, outputPos.position, Quaternion.identity);
        }
        if (item.Equals("Crank"))
        {
            craftedObject = Instantiate(Crank, outputPos.position, Quaternion.identity);
        }
        if (item.Equals("Stirrup"))
        {
            craftedObject = Instantiate(Stirrup, outputPos.position, Quaternion.identity);
        }
        if (item.Equals("Crossbow Base"))
        {
            craftedObject = Instantiate(CrossbowBase, outputPos.position, Quaternion.identity);
        }
        if (item.Equals("Crossbow"))
        {
            Destroy(minigameHeader);
        }
        if(craftedObject != null)
        {
            craftedObject.transform.SetParent(minigameHeader.transform, true);
        }
    }
    private void ClearGrid()
    {
        foreach(GameObject item in curTable)
        {
            if(item != null)
            {
                Destroy(item);
            }
        }
        Array.Clear(curTable, 0, 9);
    }
}
