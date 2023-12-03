using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * class used to handle interaction between enemies and homebase
 */
public class Homebase : MonoBehaviour
{
    // if an enemy which isn't currently dying enters the  hitbox of the base, kill the enemy, gain gears, and take damage
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Enemy")) {
            Enemy enemy = other.GetComponentInChildren<Enemy>();
            if (enemy.dying) return;
            int enemyDamage = enemy.damageToBase;
            GameManager.Instance.TakeDamage(enemyDamage);
            GameManager.Instance.AddGears(enemy.gearAddition);
            Destroy(other.gameObject);
        }
    }
}
