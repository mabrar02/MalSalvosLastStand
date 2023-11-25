using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BulletCores : ScriptableObject
{
    public float slowingAmount;
    public float slowDuration;
    public float burnDuration;
    public float burnDamage; 
    public float burnInterval;
    public float splashRadius;
    public float splashDamage;
}
