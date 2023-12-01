using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RepairUI : MonoBehaviour
{
    private Camera fpsCam;
    [SerializeField] private TurretStats turretStats;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI damageText;
    [SerializeField] private TextMeshProUGUI repairCost;
    [SerializeField] private TextMeshProUGUI upgradeCost;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        fpsCam = Camera.main;

    }

    private void OnEnable() {
        UpdateText();
    }

    private void LateUpdate() {
        if(fpsCam != null) {
            transform.LookAt(fpsCam.transform);
            transform.rotation = Quaternion.LookRotation(fpsCam.transform.forward);
        }
    }

    public void UpdateText() {
        if(turretStats.level == 4) {
            levelText.text = "LVL\nMAX";
            upgradeCost.text = string.Empty;
        }
        else {
            levelText.text = "LVL\n" + turretStats.level;
            upgradeCost.text = turretStats.upgradeCost.ToString();
        }

        healthText.text = "HP\n" + turretStats.currentHealth;
        damageText.text = "DMG\n" + turretStats.damage;
        repairCost.text = turretStats.repairCost.ToString();
    }

}
