using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ShopItem : Shoppable
{
    public HoldableItem HoldableItem;
    public int shopCost;

    public override bool OnPurchase() {
        if (HoldableItem != null && InventoryManager.Instance.AddItem(HoldableItem)) {
            return true;
        }

        return false;

    }

    public override int cost {
        get => shopCost;
        set => shopCost = value;
    }

}
