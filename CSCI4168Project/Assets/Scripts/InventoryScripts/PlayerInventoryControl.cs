using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerInventoryControl : MonoBehaviour
{
    public static PlayerInventoryControl instance;
    private Inventory playerInventory;
    private int currItem = -1;
    private Transform pivotArm;
    private AudioSource drawSoundSource;

    private void Awake()
    {
        instance = this;
        
    }

    void Start()
    {
        pivotArm = GameObject.Find("PivotArm").transform;
        playerInventory = InventoryManager.Instance.GetInventory();
        drawSoundSource = this.GameObject().GetComponent<AudioSource>();
    }
    void Update()
    {
        int item = GetItem();

        if (item != currItem)
        {
            SwitchItem(item);
        }
    }

    public int GetHeldItemIndex()
    {
        return currItem;
    }

    public void RemoveHeldItemFromInventory()
    {
        RemoveHeldItemFromPlayer();
        playerInventory.RemoveItem(currItem);
    }

    public void RemoveHeldItemFromPlayer()
    {
        int numChildren = pivotArm.childCount;
        for (int i = 0; i < numChildren; i++)
        {
            Destroy(pivotArm.GetChild(i).GameObject());
        }
    }

    private void SwitchItem(int newItemIndex)
    {
        RemoveHeldItemFromPlayer();

        HoldableItem item = playerInventory.GetItem(newItemIndex);
        if (item != null)
        {
            Instantiate(item.model, pivotArm);
            if (item.drawSound != null)
            {
                drawSoundSource.Stop();
                drawSoundSource.clip = item.drawSound;
                drawSoundSource.volume = 0.2f;
                drawSoundSource.Play();
            }
            
        }
        currItem = newItemIndex;
    }
    
    private int GetItem()
    {
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (currItem + 1 < playerInventory.Size())
            {
                return currItem + 1;
            }
            
        } 
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (currItem - 1 >= 0)
            {
                return currItem - 1;
            }
        } 
        
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            return 0;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            return 1;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            return 2;
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            return 3;
        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            return 4;
        }

        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            return 5;
        }

        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            return 6;
        }

        return currItem;
    }
    
}
