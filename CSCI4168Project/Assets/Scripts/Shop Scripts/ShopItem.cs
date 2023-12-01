using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ShopItem : Shoppable
{
    public HoldableItem HoldableItem;

    public override bool OnPurchase()
    {
        if (HoldableItem != null && InventoryManager.Instance.AddItem(HoldableItem))
        {
            return GameManager.Instance.UseGears(cost);
        }

        return false;

    }
}
