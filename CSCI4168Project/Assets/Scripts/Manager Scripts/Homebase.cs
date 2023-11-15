using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Homebase : MonoBehaviour
{
    [SerializeField] private int enemyDamage;

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Enemy")) {
            GameManager.Instance.TakeDamage(enemyDamage);
            Destroy(other.gameObject);
        }
    }
}
