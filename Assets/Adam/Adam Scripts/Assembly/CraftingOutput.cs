using UnityEngine;
using System;

public class CraftingOutput : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Transform outputPos;
    public string[][] craftingRecipes = new string[1][];
    public string[] curTable = new string[9];
    public GameObject pickaxe;

    void Start()
    {
        outputPos = transform;
        craftingRecipes[0] = new string[]{
                "Charmander", "", "",
                "", "", "", 
                "", "", ""
                            };
        Array.Fill(curTable, string.Empty);
    }

    public void addItemToTable(GameObject item, int pos)
    {
        if(curTable[pos] != null)
        {
            //remove current item
        }
        curTable[pos] = item.name;
        compareRecipes();
    }

    private void compareRecipes()
    {
        if(compareArrays(craftingRecipes[0], curTable))
        {
            Debug.Log("truth!");
            instantiateCraftedItem();
        }
        /*foreach(string[] recipe in craftingRecipes)
        {
            Debug.Log("hey");
            if(compareArrays(recipe, curTable))
            {
                instantiateCraftedItem();
            }
        }*/
    }

    private bool compareArrays(string[] recipe, string[] table)
    {
        for(int i = 0;i < 9; i++)
        {
            if(!recipe[i].Equals(table[i]))
            {
                return false;
            }
        }
        return true;
    }
    private void instantiateCraftedItem()
    {
        GameObject craftedObject = Instantiate(pickaxe, outputPos.position, Quaternion.identity);

        // 2. Set the parent
        // 'this.transform' refers to the Transform component of the parent object 
        // to which this script is attached.
        //childObject.transform.SetParent(this.transform);

        // 3. Reset local position to zero relative to the parent
        // This is the key step to ensure it's at the parent's exact location.
        //childObject.transform.localPosition = Vector3.zero;
    }
}
