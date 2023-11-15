using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Note: refered to this StackOverflow question to get the gun rotation right: https://stackoverflow.com/a/56570217

public class EnemyGuns : MonoBehaviour
{
    /* PUBLIC VARIABLES */
    public GameObject target; // should maybe be an array?
    public float rotationSpeed; //todo
    public float projectileSpeed;
    public GameObject bullet;
    public Transform gunTipTransform;
    public bool shoot;

    /* PRIVATE VARIABLES */
    private Vector3 enemyTarget;

    public int bulletDamage;



    // Start is called before the first frame update
    void Start()
    {
        shoot = false;

    }


    // Update is called once per frame
    void Update()
    {
        if (gunTipTransform != null && target != null)
        {

            
            // get target without y
            enemyTarget = target.transform.position;
            enemyTarget.y = transform.parent.position.y;
                
            // Have the enemy look at the target

            // Calculate the rotation to look at the enemy's target
            Quaternion enemyRotation = Quaternion.LookRotation(enemyTarget - transform.parent.position);

            // smoothly rotate
            transform.parent.rotation = Quaternion.Slerp(transform.parent.rotation, enemyRotation, Time.deltaTime * rotationSpeed);

            // Have the gun point at the target
            //transform.LookAt(target);
            
            
            // Calculate the rotation to look at the enemy's target
            Quaternion gunRotation = Quaternion.LookRotation(target.transform.position - transform.position);

            // smoothly rotate
            transform.rotation = Quaternion.Slerp(transform.rotation, gunRotation, Time.deltaTime * rotationSpeed);
           

            // Shoot the gun
            if (shoot)
            {
                shoot = false;
                // Create a bullet
                GameObject bulletObj = Instantiate(bullet, gunTipTransform.position, gunTipTransform.rotation);
                // set the bullet starting point
                //bullet.transform.position = gunTipTransform.position;

                // pass the target to the bullet script. 
                BulletScript bulletScript = bulletObj.GetComponent<BulletScript>();

                // Set variables on bulletScript
                bulletScript.target = target;
                bulletScript.speed = projectileSpeed;
                bulletScript.damage = bulletDamage;
            }
        }
    }
}
