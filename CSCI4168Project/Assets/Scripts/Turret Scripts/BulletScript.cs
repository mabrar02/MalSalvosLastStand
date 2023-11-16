using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    /* PUBLIC VARIABLES */
    public GameObject target;
    public float speed;
    public int damage;

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
            transform.LookAt(target.transform);
           
            // movement direction
            Vector3 movementDirection = target.transform.position - transform.position;

            // Maintains direction but sets magnitude to 1, so if you're going up and left it's not faster than going up!
            movementDirection.Normalize();

            // direction and speed is velocity
            Vector3 velocity = movementDirection * speed;

            // Move the bullet
            transform.Translate(velocity * Time.deltaTime, Space.World);

            float distanceToTarget = Vector3.Distance(target.transform.position, transform.position);

            if (distanceToTarget < 0.5f)
            {
                if (target.CompareTag("Enemy")) {
                    target.GetComponent<Enemy>().TakeDamage(damage);
                }

                Destroy(gameObject);
            }
        }
        else if (target == null) {
            Destroy(gameObject);
        }

    }





}
