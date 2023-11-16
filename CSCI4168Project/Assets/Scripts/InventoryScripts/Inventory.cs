using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Inventory : ScriptableObject
{
    public ItemInstance[] items = new ItemInstance[10];
    public bool AddItem(ItemInstance itemToAdd)
    {
        // Finds an empty slot if there is one
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] == null)
            {
                items[i] = itemToAdd;
                return true;
            }
        }

        Debug.Log("No space in the inventory");
        return false;
    }

    public bool removeItem(int index)
    {
        if (index < items.Length)
        {
            items[index] = null;
            return true;
        }
        
        return false;
    }

    public int size()
    {
        return items.Length;
    }
    
    
}
