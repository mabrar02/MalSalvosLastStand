using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

/**
 * Class describes an item that can be held in the inventory
 */
[CreateAssetMenu]
public class HoldableItem : ScriptableObject
{

    public string itemName;
    public string description;
    public Texture2D image;
    public GameObject model;
    public AudioClip drawSound;

}
