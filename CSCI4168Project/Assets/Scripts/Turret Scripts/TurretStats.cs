using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretStats : MonoBehaviour
{
    [SerializeField] AudioSource upgradeSE;
    [SerializeField] AudioSource repairSE;

    public float fireRate;
    public int damage;
    public int health;
    public int level;
    private Mesh turretMesh;

    public int currentHealth;

    [SerializeField] private TurretLevelDB turretDB;
    [SerializeField] private int repairCost;
    private int upgradeIndex = 0;

    private TargettingScript targetScript;
    private GunScript gunScript;
    public bool[] activeCores;

    void Start()
    {
        UpdateStats();

        targetScript = gameObject.GetComponent<TargettingScript>();
        gunScript = gameObject.GetComponent<GunScript>();
        activeCores = new bool[3];
    }

    public void Upgrade() {
        if (upgradeIndex + 1 >= turretDB.turretLevels.Count) {
            Debug.Log("MAX LEVEL ALREADY");
            return;
        }

        if (GameManager.Instance.UseGears(turretDB.turretLevels[upgradeIndex + 1].gearCost)) {
            upgradeIndex++;
            upgradeSE.Play();
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

    public void Repair() {
        if(currentHealth == health) {
            Debug.Log("FULL HEALTH ALREDY");
            return;
        }

        if (GameManager.Instance.UseGears(repairCost)) {
            currentHealth = health;
            repairSE.Play();
            UpdateStats();

            if (targetScript != null) {
                targetScript.SetTurretStats();
            }
            if (gunScript != null) {
                gunScript.SetTurretStats();
            }

        }
        else {
            Debug.Log("NOT ENOUGH GEARS");
        }
    }

    public void SetCore(int coreIndex) {
        if (activeCores[coreIndex] == true) {
            Debug.Log("CORE ALREADY SET!");
        }
        else {
            activeCores[coreIndex] = true;
          
        }
        if (gunScript != null) {
            gunScript.SetTurretStats();
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
