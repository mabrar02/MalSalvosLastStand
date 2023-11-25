using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventoryUI : MonoBehaviour
{

    private TextMeshProUGUI itemText;
    private PlayerInventoryControl inventoryControl;
    private Inventory playerInventory;
    private GameObject[] slots;
    private int currItemSlot;
    private Color white = new Color(1f, 1f, 1f, (float)(175.0/255.0));
    private Color green = new Color(0, 1f, 0, (float)(175.0/255.0));
    
    private void Start()
    {
        inventoryControl = PlayerInventoryControl.instance;
        playerInventory = inventoryControl.PlayerInventory;
        currItemSlot = inventoryControl.GetHeldItemIndex();
        itemText = GameObject.Find("ItemHeldText").GetComponent<TextMeshProUGUI>();

        for (int i = 0; i < playerInventory.Size(); i++)
        {
            HoldableItem item = playerInventory.GetItem(i);
            if (item != null && item.image != null)
            {
                RawImage itemImage = GetSlotItemImage(i);
                itemImage.texture = item.image;
                itemImage.color = Color.white;
            }
        }
    }

    private void Update()
    {
        if (currItemSlot != inventoryControl.GetHeldItemIndex())
        {
            if (currItemSlot >= 0 && currItemSlot < playerInventory.Size())
            {
                GetSlotImage(currItemSlot).color = white;

            }
            
            currItemSlot = inventoryControl.GetHeldItemIndex();
            GetSlotImage(currItemSlot).color = green;
            itemText.text = playerInventory.GetItem(currItemSlot).itemName;
        }
        
    }

    void OnInventoryUpdate()
    {
        
    }

    private void Awake()
    {
        if (playerInventory != null)
        {
            playerInventory.InventoryUpdate += OnInventoryUpdate;
        }
    }
    
    private void OnDestroy()
    {
        if (playerInventory != null)
        {
            playerInventory.InventoryUpdate -= OnInventoryUpdate;
        }
    }

    private Image GetSlotImage(int index)
    {
        return transform.GetChild(index).GameObject().GetComponent<Image>();
    }

    private RawImage GetSlotItemImage(int index)
    {
        return transform.GetChild(index).GetChild(0).GetComponent<RawImage>();
    }
    
}
