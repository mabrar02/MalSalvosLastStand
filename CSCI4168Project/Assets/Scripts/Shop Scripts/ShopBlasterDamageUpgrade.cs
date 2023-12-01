using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu]
public class ShopBlasterDamageUpgrade : Shoppable
{
    public int damageIncrement;

    public override bool OnPurchase()
    {
        InventoryManager.Instance.blasterDamageUpgrade += 10;
        return true;
    }
}
