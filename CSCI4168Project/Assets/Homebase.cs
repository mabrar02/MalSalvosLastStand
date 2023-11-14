using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Homebase : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Enemy")) {
            Destroy(other.gameObject);
        }
    }
}
