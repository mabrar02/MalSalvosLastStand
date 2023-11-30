using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasePlayer : MonoBehaviour
{
    /* PUBLIC VARIABLES */
    public float shootingInterval; // how long between shots
    public string tagToTarget;


    /* PRIVATE VARIABLES */
    public GameObject target; // all the targets
    private float shotTimer = 0.0f; // time since you last shot
    private int currentTargetIndex = 0; // which target your need to shoot at next
    private EnemyGuns gunScript; // the gun script
    private MoveTo moveToScript;


    // Start is called before the first frame update
    void Start()
    {
        gunScript = GetComponent<EnemyGuns>(); 
        moveToScript = GetComponentInParent<MoveTo>();
    }


    // Update is called once per frame
    void Update()
    {

        // if there are any enemies to target
        if (target != null)
        {
            // if the timer has gone over the interval
            if (shotTimer >= shootingInterval)
            {
                //currentTargetIndex = currentTargetIndex < targets.Count ? currentTargetIndex : 0;
                // shoot the gun
                shootGun(target);

                // move to next target in list or go back to 0
                //currentTargetIndex = (currentTargetIndex + 1) % targets.Count;

                // reset timer
                shotTimer = 0.0f;
            }
            else
            {
                // increment the timer
                shotTimer += Time.deltaTime;
            }

        }
        else
        {
            lookAtBase();
        }
    }

    private void shootGun(GameObject target)
    {
        gunScript.target = target;
        gunScript.shoot = true;
    }

    private void lookAtBase()
    {
        gunScript.target = moveToScript.goal.gameObject;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == tagToTarget)
        {
            Debug.Log("ENTERED");
            target = other.gameObject;
            moveToScript.goal = target.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == tagToTarget)
        {
            target = null;
        }
    }
}
