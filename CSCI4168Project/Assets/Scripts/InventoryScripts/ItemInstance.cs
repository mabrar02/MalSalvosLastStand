using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemInstance
{
    public HoldableItem item;

    public ItemInstance(HoldableItem item)
    {
        this.item = item;
    }
}
