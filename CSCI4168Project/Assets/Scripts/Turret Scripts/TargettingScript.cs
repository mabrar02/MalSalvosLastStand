using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargettingScript : MonoBehaviour
{
    /* PUBLIC VARIABLES */
    public float shootingInterval; // how long between shots
    public string tagToTarget;


    /* PRIVATE VARIABLES */
    public List<GameObject> targets; // all the targets
    private float shotTimer = 0.0f; // time since you last shot
    private int currentTargetIndex = 0; // which target your need to shoot at next
    private GunScript gunScript; // the gun script

    private TurretStats turretStats;
    // Start is called before the first frame update
    void Start() {
        gunScript = GetComponent<GunScript>(); // find the gun script
        targets = new List<GameObject>();

        turretStats = GetComponent<TurretStats>();

        if (turretStats != null) {
            Debug.Log("TURRET STATS FOUND");
            SetTurretStats();
        }
        else {
            Debug.Log("TURRET STATS BROKEN");
        }

    }

    public void SetTurretStats() {
        shootingInterval = turretStats.fireRate;

    }

    // Update is called once per frame
    void Update()
    {
        // if there are any enemies to target
        if (targets.Count > 0)
        {
            if (targets[0] == null) targets.Remove(targets[0]);
            else {
                // if the timer has gone over the interval
                if (shotTimer >= shootingInterval) {
                    //currentTargetIndex = currentTargetIndex < targets.Count ? currentTargetIndex : 0;
                    // shoot the gun
                    shootGun(targets[0]);

                    // move to next target in list or go back to 0
                    //currentTargetIndex = (currentTargetIndex + 1) % targets.Count;

                    // reset timer
                    shotTimer = 0.0f;
                }
                else {
                    // increment the timer
                    shotTimer += Time.deltaTime;
                }
            }


        }
    }

    private void shootGun(GameObject target)
    {
        gunScript.target = target;
        gunScript.shoot = true;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == tagToTarget)
        {
            targets.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == tagToTarget)
        {
            targets.Remove(other.gameObject);
        }
    }
}
