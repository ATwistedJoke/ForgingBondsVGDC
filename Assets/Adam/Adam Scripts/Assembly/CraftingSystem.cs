/*using UnityEngine;
using System;
using System.Collections.Generic;

public class CraftingSystem : MonoBehaviour
{
    private const int GRID_SIZE = 3;
    private Item[,] grid;

    public event Action OnCraftingGridChanged;
    public Item outputItem;

    public CraftingSystem()
    {
        grid = new Item[GRID_SIZE, GRID_SIZE];
    }
    
    public Item GetItem(int x, int y) => grid[x, y];
    public void SetItem(Item it, int x, int y)
    {
        grid[x, y] = GetItem;
        OnCraftingGridChanged?.Invoke();
        UpdateOutput();
    }
    public void RemoveItem(int x, int y)
    {
        grid[x, y] = null;
        OnCraftingGridChanged?.Invoke();
    }

    private void UpdateOutput()
    {
        outputItem = GetRecipeOutput();
    }

    private Item GetRecipeOutput()
    {
        var axeRecipe = new String[,]
        {
            {"Pleotium", "Pleotium", "Pleotium"},
            {null, "Stick", "Pleotium"},
            {null, "Stick", null}
        };

        if(MatchRecipe(axeRecipe)) return new Item(ItemType.axe, 1);
        return null;
    }

    private bool MatchRecipe(String [,] recipe)
    {
        for(int x = 0; x < GRID_SIZE; x++)
        {
            for (int y = 0; y < GRID_SIZE; y++)
            {
                if(!grid[x, y]?.getItem().equals(recipe[x, y]) && recipe[x, y] != null)
                {
                    return false;
                }
            }
        }
        return true;
    }
}
*/