using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTargetting : MonoBehaviour
{
    /* PUBLIC VARIABLES */
    public float shootingInterval; // how long between shots
    public string tagToTarget;


    /* PRIVATE VARIABLES */
    public List<GameObject> targets; // all the targets
    private float shotTimer = 0.0f; // time since you last shot
    private EnemyGuns gunScript; // the gun script
    private GameObject goal;


    // Start is called before the first frame update
    void Start()
    {
        gunScript = GetComponent<EnemyGuns>(); // find the gun script
        targets = new List<GameObject>();
        goal = GetComponentInParent<MoveTo>().goal.gameObject;
    }


    // Update is called once per frame
    void Update()
    {

        // if there are any enemies to target
        if (targets.Count > 0)
        {
            if (targets[0].GetComponentInChildren<TurretStats>().disabled) targets.Remove(targets[0]);
            // if the timer has gone over the interval
            if (shotTimer >= shootingInterval)
            {
                //currentTargetIndex = currentTargetIndex < targets.Count ? currentTargetIndex : 0;
                // shoot the gun
                shootGun(targets[0]);

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
        gunScript.target = goal;
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
