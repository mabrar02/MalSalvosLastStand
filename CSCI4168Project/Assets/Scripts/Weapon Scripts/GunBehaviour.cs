using System;
using UnityEngine;

/*
 * Gun Behaviour Class
 * Controls the behaviour of the guns in the game.
 * Code adapted from https://learn.unity.com/tutorial/let-s-try-shooting-with-raycasts#
 */
public class GunBehaviour : MonoBehaviour {

    public int gunDamage = 1;                                            // Set the number of hitpoints that this gun will take away from shot objects with a health script
    public float fireRate = 0.25f;                                        // Number in seconds which controls how often the player can fire
    public float weaponRange = 50f;                                        // Distance in Unity units over which the player can fire
    public float hitForce = 100f;                                        // Amount of force which will be added to objects with a rigidbody shot by the player
    public Transform gunEnd;                                            // Holds a reference to the gun end object, marking the muzzle location of the gun
                                                   
    public AudioSource gunSound;
    public ParticleSystem muzzleFlash;
    
    private Camera fpsCam;
    private float _nextFire;                                                // Float to store the time the player will be allowed to fire again, after firing

    void Start()
    {
        fpsCam = Camera.main;
    }

    void Update () 
    {
        muzzleFlash.Stop();
        // Check if the player has pressed the fire button and if enough time has elapsed since they last fired
        if (Input.GetButtonDown("Fire1") && Time.time > _nextFire) 
        {
            
            // Update the time when our player can fire next
            _nextFire = Time.time + fireRate;

            // Create a vector at the center of our camera's viewport
            Vector3 rayOrigin = fpsCam.ViewportToWorldPoint (new Vector3(0.5f, 0.5f, 0.0f));

            // Declare a raycast hit to store information about what our raycast has hit
            RaycastHit hit;
    
            gunSound.Play();
            muzzleFlash.Play();
            // Check if our raycast has hit anything
            if (Physics.Raycast (rayOrigin, fpsCam.transform.forward, out hit, weaponRange))
            {
                Enemy enemy = hit.collider.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.Damage(gunDamage);
                }
                
                
                // Check if the object we hit has a rigidbody attached
                if (hit.rigidbody != null)
                {
                    // Add force to the rigidbody we hit, in the direction from which it was hit
                    hit.rigidbody.AddForce (-hit.normal * hitForce);
                }
            }
        }
    }
}
