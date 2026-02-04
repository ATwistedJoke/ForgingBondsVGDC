using UnityEngine;
using System;

public class CraftingOutput : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Transform outputPos;
    public string[][] craftingRecipes = new string[1][];
    public GameObject[] curTable;
    public GameObject pickaxe;
    public DragObject dragObject;

    void Start()
    {
        outputPos = transform;
        craftingRecipes[0] = new string[]{
                "Charmander", "", "",
                "", "", "", 
                "", "", ""
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
        dragObject = toRemove.GetComponent<DragObject>();
        dragObject.returnToStartPos();
        curTable[pos] = null;
    }
    private void compareRecipes()
    {
        foreach(string[] recipe in craftingRecipes)
        {
            if(compareArrays(craftingRecipes[0], curTable))
            {
                instantiateCraftedItem();
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
                if(!recipe[i].Equals(table[i].name))
                {
                    return false;
                }
            }
        }
        return true;
    }
    private void instantiateCraftedItem()
    {
        GameObject craftedObject = Instantiate(pickaxe, outputPos.position, Quaternion.identity);
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
