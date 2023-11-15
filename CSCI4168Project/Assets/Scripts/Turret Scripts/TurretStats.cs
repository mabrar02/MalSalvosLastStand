using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretStats : MonoBehaviour
{


    public float fireRate;
    public int damage;
    public int health;
    private int level;
    private Mesh turretMesh;

    [SerializeField] private TurretLevelDB turretDB;
    

    void Start()
    {
        fireRate = turretDB.turretLevels[0].fireRate;
        damage = turretDB.turretLevels[0].damage;
        health = turretDB.turretLevels[0].health;
        level = turretDB.turretLevels[0].level;
        turretMesh = turretDB.turretLevels[0].turretMesh;

    }



}
