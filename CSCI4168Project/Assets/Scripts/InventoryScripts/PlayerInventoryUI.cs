using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventoryUI : MonoBehaviour
{
    
    private Inventory _playerInventory;
    private GameObject[] _slots;

    private void Start()
    {
        
    }

    void OnInventoryUpdate()
    {
        
    }

    private void Awake()
    {
        if (_playerInventory != null)
        {
            _playerInventory.InventoryUpdate += OnInventoryUpdate;
        }
    }
    
    private void OnDestroy()
    {
        if (_playerInventory != null)
        {
            _playerInventory.InventoryUpdate -= OnInventoryUpdate;
        }
    }
}
