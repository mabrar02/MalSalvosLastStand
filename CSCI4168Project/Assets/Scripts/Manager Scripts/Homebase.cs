using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Homebase : MonoBehaviour
{

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Enemy")) {
            int enemyDamage = other.GetComponentInChildren<Enemy>().damageToBase;
            GameManager.Instance.TakeDamage(enemyDamage);
            Destroy(other.gameObject);
        }
    }
}
