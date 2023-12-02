using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Homebase : MonoBehaviour
{

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Enemy")) {
            Enemy enemy = other.GetComponentInChildren<Enemy>();
            if (enemy.dying) return;
            int enemyDamage = enemy.damageToBase;
            GameManager.Instance.TakeDamage(enemyDamage);
            Destroy(other.gameObject);
        }
    }
}
