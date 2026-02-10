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

    void Start()
    {
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
                "Ring", "", "", "Shaft"
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
        if (item.Equals("Handle"))
        {
            GameObject craftedObject = Instantiate(Handle, outputPos.position, Quaternion.identity);
        }
        if (item.Equals("Pleiotium Cylinder"))
        {
            GameObject craftedObject = Instantiate(PleiotiumCylinder, outputPos.position, Quaternion.identity);
        }
        if (item.Equals("Morningstar Head"))
        {
            GameObject craftedObject = Instantiate(MorningstarHead, outputPos.position, Quaternion.identity);
        }
        if (item.Equals("Shaft"))
        {
            GameObject craftedObject = Instantiate(Shaft, outputPos.position, Quaternion.identity);
        }
        if (item.Equals("Morningstar"))
        {
            GameObject craftedObject = Instantiate(Morningstar, outputPos.position, Quaternion.identity);
        }
        if (item.Equals("Bow"))
        {
            GameObject craftedObject = Instantiate(Bow, outputPos.position, Quaternion.identity);
        }
        if (item.Equals("Crank"))
        {
            GameObject craftedObject = Instantiate(Crank, outputPos.position, Quaternion.identity);
        }
        if (item.Equals("Stirrup"))
        {
            GameObject craftedObject = Instantiate(Stirrup, outputPos.position, Quaternion.identity);
        }
        if (item.Equals("Crossbow Base"))
        {
            GameObject craftedObject = Instantiate(CrossbowBase, outputPos.position, Quaternion.identity);
        }
        if (item.Equals("Crossbow"))
        {
            GameObject craftedObject = Instantiate(Crossbow, outputPos.position, Quaternion.identity);
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
