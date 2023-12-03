using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu]
public class ShopHealthUpgrade : Shoppable
{
    public int shopCost;
    public int increaseAmount;
    public bool playerIncrease;
    public bool baseIncrease;


    public override bool OnPurchase()
    {
        if(baseIncrease) {
            AudioManager.Instance.Play("ShopBuy");
            GameManager.Instance.IncreaseHomebaseHealth(increaseAmount);
            return true;
        }
        else if (playerIncrease) {
            AudioManager.Instance.Play("ShopBuy");
            GameManager.Instance.IncreasePlayerHealth(increaseAmount);
            return true;
        }
        else {
            return false;
        }
    }

    public override int cost {
        get => shopCost;
        set => shopCost = value;
    }
}
