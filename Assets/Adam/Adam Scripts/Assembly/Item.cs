using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Item : MonoBehaviour
{
    public enum ItemType
    {
        Stick,
        Pleotium,
        Axe,
    }
    public ItemType item;
    public string getItem()
    {
        return item.ToString();
    }
}
