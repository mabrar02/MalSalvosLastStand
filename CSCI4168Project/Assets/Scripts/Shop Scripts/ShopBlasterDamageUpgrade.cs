using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu]
public class ShopBlasterDamageUpgrade : Shoppable
{
    public int damageIncrement;
    public int shopCost;
    public int increaseIncrement;

    public override bool OnPurchase()
    {
        InventoryManager.Instance.blasterDamageUpgrade += 10;
        shopCost += increaseIncrement;
        return true;
    }

    public override int cost { 
        get => shopCost;
        set => shopCost = value; 
    }
}
