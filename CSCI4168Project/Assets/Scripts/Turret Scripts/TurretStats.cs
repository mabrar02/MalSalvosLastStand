using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TurretStats : MonoBehaviour
{
    [SerializeField] AudioSource upgradeSE;
    [SerializeField] AudioSource repairSE;
    [SerializeField] AudioSource breakSE;

    public float fireRate;
    public int damage;
    public int health;
    public int level;
    private Mesh turretMesh;
    public MeshRenderer baseMesh;

    public int currentHealth;

    [SerializeField] private TurretLevelDB turretDB;
    [SerializeField] private int repairCost;
    private int upgradeIndex = 0;

    private TargettingScript targetScript;
    private GunScript gunScript;
    public bool[] activeCores;
    public bool disabled;

    void Start()
    {
        UpdateStats();

        targetScript = gameObject.GetComponent<TargettingScript>();
        gunScript = gameObject.GetComponent<GunScript>();
        disabled = false;
        activeCores = new bool[4];
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
            EnableTower();

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

    public bool SetCore(int coreIndex) {
        if (activeCores[coreIndex] == true) {
            Debug.Log("CORE ALREADY SET!");
            return false;
        }
        else {
            activeCores[coreIndex] = true;
          
        }
        if (gunScript != null) {
            gunScript.SetTurretStats();
        }
        return true;
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

    public void TakeDamage(int damage) {
        currentHealth -= damage;
        if(currentHealth <= 0) {
            currentHealth = 0;
            DisableTower();
        }
    }

    public void DisableTower() {
        breakSE.Play();
        targetScript.enabled = false;
        gunScript.enabled = false;
        disabled = true;
        baseMesh.material.SetColor("_Color", Color.red);
    }

    public void EnableTower() {
        targetScript.enabled = true;
        gunScript.enabled = true;
        disabled = false;

        baseMesh.material.SetColor("_Color", Color.white);
    }


}
