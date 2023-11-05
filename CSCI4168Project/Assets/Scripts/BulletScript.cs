using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    /* PUBLIC VARIABLES */
    public Transform target;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null && speed>0)
        {
            // point at target
            transform.LookAt(target);
            // make sure it's rotated at the right angle
            transform.Rotate(Vector3.right * 90);
            // movement direction
            Vector3 movementDirection = target.position - transform.position;

            // Maintains direction but sets magnitude to 1, so if you're going up and left it's not faster than going up!
            movementDirection.Normalize();

            // direction and speed is velocity
            Vector3 velocity = movementDirection * speed;

            // Move the bullet
            transform.Translate(velocity * Time.deltaTime, Space.World);

            float distanceToTarget = Vector3.Distance(target.transform.position, transform.position);

            if (distanceToTarget < 10)
            {
                Destroy(gameObject);
            }
        }

    }


}
