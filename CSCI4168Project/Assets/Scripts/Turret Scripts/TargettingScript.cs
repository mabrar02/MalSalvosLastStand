using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargettingScript : MonoBehaviour
{
    /* PUBLIC VARIABLES */
    public float shootingInterval; // how long between shots
    public string tagToTarget;


    /* PRIVATE VARIABLES */
    private List<Transform> transforms; // all the targets
    private float shotTimer = 0.0f; // time since you last shot
    private int currentTargetIndex = 0; // which target your need to shoot at next
    private GunScript gunScript; // the gun script

    // Start is called before the first frame update
    void Start()
    {
        gunScript = GetComponent<GunScript>(); // find the gun script
        transforms = new List<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        // if there are any enemies to target
        if (transforms.Count > 0)
        {
            // if the timer has gone over the interval
            if (shotTimer >= shootingInterval)
            {
                currentTargetIndex = currentTargetIndex < transforms.Count ? currentTargetIndex : 0;
                // shoot the gun
                shootGun(transforms[currentTargetIndex]);

                // move to next target in list or go back to 0
                currentTargetIndex = (currentTargetIndex + 1) % transforms.Count;

                // reset timer
                shotTimer = 0.0f;
            }
            else
            {
                // increment the timer
                shotTimer += Time.deltaTime;
            }

        }
    }

    private void shootGun(Transform target)
    {
        gunScript.target = target;
        gunScript.shoot = true;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == tagToTarget)
        {
            transforms.Add(other.transform);
            Debug.Log("Entering " + other.tag + " len:" + transforms.Count);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == tagToTarget)
        {
            transforms.Remove(other.transform);
            Debug.Log("Exiting "+other.tag+" len:"+transforms.Count);
        }
    }
}
