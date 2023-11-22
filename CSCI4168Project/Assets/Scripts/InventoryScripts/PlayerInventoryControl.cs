using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerInventoryControl : MonoBehaviour
{
    public static PlayerInventoryControl instance;
    [SerializeField] public Inventory PlayerInventory;
    public event Action ItemChange;
    private int currItem = -1;
    private Transform pivotArm;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        pivotArm = GameObject.Find("PivotArm").transform;
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

    private void SwitchItem(int newItemIndex)
    {
        int numChildren = pivotArm.childCount;
        for (int i = 0; i < numChildren; i++)
        {
            Destroy(pivotArm.GetChild(i).GameObject());
        }

        if (PlayerInventory.GetItem(newItemIndex) != null)
        {
            Instantiate(PlayerInventory.GetItem(newItemIndex).model, pivotArm);
            ItemChange?.Invoke();
        }
        currItem = newItemIndex;
    }
    
    private int GetItem()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (currItem + 1 < PlayerInventory.Size())
            {
                return currItem + 1;
            }
            
        } 
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
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

        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            return 7;
        }

        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            return 8;
        }

        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            return 9;
        }

        return currItem;
    }
    
}
