using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

[CreateAssetMenu]
public class HoldableItem : ScriptableObject
{

    public string itemName;
    public string description;
    public Texture2D image;
    public GameObject model;

}
