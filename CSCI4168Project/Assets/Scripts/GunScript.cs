using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Note: refered to this StackOverflow question to get the gun rotation right: https://stackoverflow.com/a/56570217

public class gunScript : MonoBehaviour
{
    /* PUBLIC VARIABLES */
    public Transform target;
    public float rotationSpeed;
    public float projectileSpeed; // bullet will need it's own script...


    /* PRIVATE VARIABLES */
    private Transform gunTransform;
    

    // Start is called before the first frame update
    void Start()
    {
        gunTransform = this.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (gunTransform != null && target != null)
        {
            // Have the gun point at the target
            gunTransform.LookAt(target);
            // make sure it's rotated at the right angle
            transform.Rotate(Vector3.right * 90);

            // Create a projectile and start it 
        }
    }
}
