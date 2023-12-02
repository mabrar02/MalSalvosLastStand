using System;
using UnityEngine;

public class GunBehaviour : MonoBehaviour
{
    public int gunDamage = 1;
    public float fireRate = 0.25f;
    public float weaponRange = 50f;
    public float hitForce = 100f;
    public Transform gunEnd;
    private Camera fpsCam;
    public AudioSource gunSound;
    public ParticleSystem muzzleFlash;
    private float _nextFire;
    

    private void Start(){
        fpsCam = Camera.main;
    }

    void Update()
    {
        muzzleFlash.Stop();

        if (Input.GetButtonDown("Fire1") && Time.time > _nextFire)
        {
            _nextFire = Time.time + fireRate;

            // Use the camera position as the ray origin
            Vector3 rayOrigin = fpsCam.transform.position;

            RaycastHit hit;

            // Perform the raycast
            int excludeLayers = LayerMask.GetMask("ground", "path", "Triggers", "Towers");
            if (Physics.Raycast(rayOrigin, fpsCam.transform.forward, out hit, weaponRange, ~excludeLayers))
            {

                if (hit.transform.CompareTag("Enemy"))
                {
                    Enemy enemy = hit.transform.GetComponent<Enemy>();
                    AudioManager.Instance.Play("HitMarker");
                    enemy.TakeDamage(gunDamage + InventoryManager.Instance.blasterDamageUpgrade);
                }

                // Uncomment this part if you want to add force to the hit object
                // if (hit.rigidbody != null)
                // {
                //     hit.rigidbody.AddForce(-hit.normal * hitForce);
                // }
            }

            gunSound.Play();
            muzzleFlash.Play();
        }
    }

}
