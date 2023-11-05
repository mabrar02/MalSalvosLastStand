using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Note: refered to this StackOverflow question to get the gun rotation right: https://stackoverflow.com/a/56570217

public class gunScript : MonoBehaviour
{
    /* PUBLIC VARIABLES */
    public Transform target; // should maybe be an array?
    public float rotationSpeed;
    public float projectileSpeed; // bullet will need it's own script...
    public GameObject bullet;
    public Transform gunTipTransform;

    /* PRIVATE VARIABLES */



    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (gunTipTransform != null && target != null)
        {
            // Have the gun point at the target
            transform.LookAt(target);
            // make sure it's rotated at the right angle
            transform.Rotate(Vector3.right * 90);

            

            if (Input.GetButtonDown("Fire1"))
            {
                // Create a bullet
                Instantiate(bullet);
                // set the bullet starting point
                bullet.transform.position = gunTipTransform.position;

                // pass the target to the bullet script. 
                BulletScript bulletScript = bullet.GetComponent<BulletScript>();

                // Set variables on bulletScript
                bulletScript.target = target;
                bulletScript.speed = projectileSpeed;
            }
        }
    }
}
