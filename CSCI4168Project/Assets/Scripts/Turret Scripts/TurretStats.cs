using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretStats : MonoBehaviour
{


    public float fireRate;
    public int damage;
    public int health;
    public int level;
    private Mesh turretMesh;

    public int currentHealth;

    [SerializeField] private TurretLevelDB turretDB;
    private int upgradeIndex = 0;

    private TargettingScript targetScript;
    private GunScript gunScript;
    

    void Start()
    {
        UpdateStats();

        targetScript = gameObject.GetComponent<TargettingScript>();
        gunScript = gameObject.GetComponent<GunScript>();
    }

    public void Upgrade() {
        if (upgradeIndex + 1 >= turretDB.turretLevels.Count) {
            Debug.Log("MAX LEVEL ALREADY");
            return;
        }

        if (GameManager.Instance.UseGears(turretDB.turretLevels[upgradeIndex + 1].gearCost)) {
            upgradeIndex++;
            UpdateStats();

            if(targetScript != null) {
                targetScript.SetTurretStats();
            }
            if(gunScript != null) {
                gunScript.SetTurretStats();
            }

        }
        else {
            Debug.Log("NOT ENOUGH GEARS");
        }
    }

    private void UpdateStats() {
        fireRate = turretDB.turretLevels[upgradeIndex].fireRate;
        damage = turretDB.turretLevels[upgradeIndex].damage;
        health = turretDB.turretLevels[upgradeIndex].health;
        level = turretDB.turretLevels[upgradeIndex].level;
        turretMesh = turretDB.turretLevels[upgradeIndex].turretMesh;

        gameObject.GetComponent<MeshFilter>().mesh = turretMesh;

        currentHealth = health;
    }



}
