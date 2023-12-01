using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public Inventory playerStartingInventory;
    private Inventory playerInventory;
    private List<TurretCore> cores;
    private void Awake()
    {
        Instance = this;
        playerInventory = Instantiate(playerStartingInventory);
    }
    
    public void AddItem(HoldableItem item)
    {
        playerInventory.AddItem(new ItemInstance(item));
    }

    public Inventory GetInventory()
    {
        return playerInventory;
    }
}
