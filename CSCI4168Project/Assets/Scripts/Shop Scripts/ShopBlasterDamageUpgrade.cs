using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu]
public class ShopBlasterDamageUpgrade : Shoppable
{
    public int damageIncrement;
    public int shopCost;

    public override bool OnPurchase()
    {
        AudioManager.Instance.Play("ShopBuy");
        InventoryManager.Instance.blasterDamageUpgrade += damageIncrement;
        return true;
    }

    public override int cost { 
        get => shopCost;
        set => shopCost = value; 
    }
}
