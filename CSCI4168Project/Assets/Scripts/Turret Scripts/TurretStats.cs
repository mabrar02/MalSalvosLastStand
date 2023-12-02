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
    public int repairCost;
    public int upgradeCost;
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
            MenuManager.Instance.SetError("Max level already!");
            return;
        }

        if (GameManager.Instance.UseGears(upgradeCost)) {
            upgradeIndex++;
            upgradeSE.Play();
            UpdateStats();
            EnableTower();

            if (targetScript != null) {
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
            MenuManager.Instance.SetError("Full health already!");
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
            MenuManager.Instance.SetError("Core already active!");
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
        if(level == 4) {
            upgradeCost = -1;
        }
        else {
            upgradeCost = turretDB.turretLevels[upgradeIndex + 1].gearCost;
        }

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
