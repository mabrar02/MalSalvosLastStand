using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu]
public class ShopHealthUpgrade : Shoppable
{
    public int shopCost;
    public int increaseAmount;
    public int increaseIncrement;
    public bool playerIncrease;
    public bool baseIncrease;
    private void Start() {
        this.cost = shopCost;
    }

    public override bool OnPurchase()
    {
        if(baseIncrease) {
            GameManager.Instance.IncreaseHomebaseHealth(increaseAmount);
            shopCost += increaseIncrement;
            return true;
        }
        else if (playerIncrease) {
            GameManager.Instance.IncreasePlayerHealth(increaseAmount);
            shopCost += increaseIncrement;
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
