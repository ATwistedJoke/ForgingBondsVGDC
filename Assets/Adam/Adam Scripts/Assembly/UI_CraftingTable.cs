/*using UnityEngine;
using System;
using System.Collections.Generic;
public class UI_CraftingTable : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private Transform[] slotTransforms;
    [SerializeField] private Transform outputTransform;
    [SerializeField] private GameObject itemPrefab;
    private CraftingSystem craftingSystem;
    public void SetCraftingSystem(CraftingSystem system)
    {
        craftingSystem = system;
        craftingSystem.OnCraftingGridChanged += RefreshGrid;
        RefreshGrid();
    }
    private void RefreshGrid()
    {
        foreach(Transform slot in slotTransforms)
        {
            foreach(Transform child in slot) Destroy(child.gameObject);
        }

        for(int x = 0;x < 3; x++)
        {
            for(int y = 0;y<3;y++)
            {
                Item item = craftingSystem.GetItem(x, y);
                if(item != null)
                {
                    GameObject itemGo = Instantiate(itemPrefab, slotTransforms[x + y * 3]);
                    itemGo.GetComponent<ItemUI>().SetItem(item);
                }
            }
        }

        foreach (Transfor child in outputTransform) Destroy(child.gameObject);
        if(craftingSystem.outputItem != null)
        {
            GameObject outputGO = Instantiate(itemPrefab, outputTransform);
            outputGO.GetComponent<ItemUI>().SetItem(craftingSystem.outputItem);
        }
    }
}
*/