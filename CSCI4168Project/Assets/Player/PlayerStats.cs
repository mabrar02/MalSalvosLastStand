using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private int gears;
    [SerializeField] TextMeshProUGUI gearText;

    private void Update() {
        gearText.text = gears.ToString();
    }
    public bool UseGears(int cost) {
        if (gears - cost >= 0) {
            gears -= cost;
            return true;
        }
        else return false;
    }

    public void AddGears(int bonus) {
        gears += bonus;
    }

    public int GetGears() {
        return gears;
    }
}
