using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Shoppable : ScriptableObject
{
    public string itemName;
    public string itemDescription;
    public abstract int cost {
        get;
        set;
    }

    public abstract bool OnPurchase();
    
}
