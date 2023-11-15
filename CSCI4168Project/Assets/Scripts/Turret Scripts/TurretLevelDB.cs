using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class TurretLevelDB : ScriptableObject
{
    public List<TurretLevel> turretLevels;
}

[Serializable]
public class TurretLevel {
    [field: SerializeField]
    public int level {  get; private set; }

    [field: SerializeField]
    public int health { get; private set; }

    [field: SerializeField]
    public int damage { get; private set; }

    [field: SerializeField]
    public float fireRate { get; private set; }

    [field: SerializeField]
    public int gearCost { get; private set; }

}
