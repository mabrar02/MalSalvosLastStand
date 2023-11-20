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
        levelText.text = "LVL\n" + turretStats.level;
        healthText.text = "HP\n" + turretStats.currentHealth;
        damageText.text = "DMG\n" + turretStats.damage;
    }

}
