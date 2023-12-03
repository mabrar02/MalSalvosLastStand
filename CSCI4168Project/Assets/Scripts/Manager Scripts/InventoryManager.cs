using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/**
 * class is used to manage the plays inventory
 */
public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public Inventory playerStartingInventory;
    public int blasterDamageUpgrade = 0;
    private Inventory playerInventory;
    private List<TurretCore> cores;
    private void Awake()
    {
        Instance = this;
        playerInventory = Instantiate(playerStartingInventory);
    }

    private void OnDisable()
    {
        blasterDamageUpgrade = 0;
    }

    // add an item to the players inventory
    public bool AddItem(HoldableItem item)
    {
        return playerInventory.AddItem(new ItemInstance(item));
    }

    public Inventory GetInventory()
    {
        return playerInventory;
    }

    // get the first item in the players inventory
    public HoldableItem GetFirstItem()
    {
        return playerInventory.GetItem(0);
    }
}
