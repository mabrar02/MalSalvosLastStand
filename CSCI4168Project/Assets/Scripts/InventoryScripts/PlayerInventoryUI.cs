using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerInventoryUI : MonoBehaviour
{
    struct InventorySlot
    {
        public InventorySlot(GameObject slotObject, Sprite sprite)
        {
            this.slotObject = slotObject;
            this.sprite = sprite;
        }
        public GameObject slotObject;
        public Sprite sprite;
    }

    private PlayerInventoryControl inventoryControl;
    private Inventory playerInventory;
    private InventorySlot[] slots;
    private int currItemSlot;
    
    private void Start()
    {
        inventoryControl = PlayerInventoryControl.instance;
        playerInventory = inventoryControl.PlayerInventory;
        slots = new InventorySlot[playerInventory.Size()];
        
    }

    void OnInventoryUpdate()
    {
        
    }

    void OnItemChange()
    {
        currItemSlot = inventoryControl.GetHeldItemIndex();
    }

    private void Awake()
    {
        if (playerInventory != null)
        {
            playerInventory.InventoryUpdate += OnInventoryUpdate;
            inventoryControl.ItemChange += OnItemChange;
        }
    }
    
    private void OnDestroy()
    {
        if (playerInventory != null)
        {
            playerInventory.InventoryUpdate -= OnInventoryUpdate;
            inventoryControl.ItemChange -= OnItemChange;
        }
    }
}
