using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Homebase : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision) {
        if (collision.transform.CompareTag("Enemy")) {
            Debug.Log("ENEMY HIT BASE");
            Destroy(collision.gameObject);
        }
    }
}
