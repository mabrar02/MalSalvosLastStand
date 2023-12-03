using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

/**
 * This class represents general enemy characteristics
 */
public class Enemy : MonoBehaviour
{

    public float maxHealth;
    public float deathTime=2.0f;
    public float _health;
    public GameObject floatingTextPref;
    public NavMeshAgent _agent;
    public float agentSpeedOriginal;
    public bool dying;

    private bool slowing;
    private bool burning;
    private SkinnedMeshRenderer[] skinRenderers;
    [SerializeField] private float colorIntensity;
    public int gearAddition;
    [SerializeField] private AudioSource deathSE;
    public int damageToBase;

    private EnemyGuns enemyAttackScript;

    private Animator animator;
    void Start()
    {
        //initialize variables
        _health = maxHealth;
        animator = GetComponentInChildren<Animator>();
        _agent = GetComponent<NavMeshAgent>();

        skinRenderers = GetComponentsInChildren<SkinnedMeshRenderer>();

        slowing = false;
        burning = false;
        dying = false;
    }

    // reduce the enemies health, if it goes past 0 initiate the die method
    public void TakeDamage(float damage)
    {
        if (dying) return;
        _health -= damage;
        if (_health <= 0)
        {
            dying = true;
            _agent.speed = 0;
            die();
        }
        else {
            ShowFloatingText();
        }

        void die()
        {
            // award player with gears upon enemy death, remove the object after sound effect plays
            GameManager.Instance.AddGears(gearAddition);
            deathSE.Play();
            Destroy(gameObject, deathTime);
            animator.SetTrigger("Die");
            
        }
    }

    // damage indicator is instantiated every time the enemy takes damage
    private void ShowFloatingText() {
        var text = Instantiate(floatingTextPref, transform.position, Quaternion.identity, transform);
        text.GetComponent<TextMesh>().text = _health.ToString();

    }

    // if the enemy isn't currently getting slowed, reduce their speed
    public void ChangeEnemySpeedCoroutine(float speedReduction, float duration) {
        if(!slowing) StartCoroutine(ChangeEnemySpeed(speedReduction, duration));
    }

    // if the enemy isn't currently getting burned, reduce their damage
    public void DamageOverTimeCoroutine(float damageAmount, float duration, float damageInterval) {
        if (!burning) StartCoroutine(DamageOverTime(damageAmount, duration, damageInterval));
    }

    // reduce speed and turn emission to blue to indicate slowing
    public IEnumerator ChangeEnemySpeed(float speedReduction, float duration) {
        slowing = true;
        ChangeEmission(Color.blue, colorIntensity);

        // slow until slow duration expired
        if (_agent != null) {
            _agent.speed -= speedReduction;
            if(_agent.speed <= 1) {
                _agent.speed = 1;
            }

            yield return new WaitForSeconds(duration);

            _agent.speed = agentSpeedOriginal;
            

        }

        ChangeEmission(Color.black, colorIntensity);
        slowing = false;
        yield return null;
    }

    // changes the emission color on enemy materials, black resets it
    public void ChangeEmission(Color emissionColor, float intensity) {
        foreach(SkinnedMeshRenderer render in skinRenderers) {
            Material[] mats = render.materials;
            foreach(Material mat in mats) {
                mat.EnableKeyword("_EMISSION");
                if(emissionColor == Color.black) {
                    mat.SetColor("_EmissionColor", Color.black);
                }
                else {
                    mat.SetColor("_EmissionColor", emissionColor * intensity);
                }
            }
        }
    }

    // deal damage over time in increments
    public IEnumerator DamageOverTime(float damageAmount, float duration, float damageInterval) {
        burning = true;
        ChangeEmission(Color.red, colorIntensity);
        float elapsedTime = 0f;
        
        // continously elapse time until burn duration is completed
        while(elapsedTime < duration) {
            TakeDamage(damageAmount);

            yield return new WaitForSeconds(damageInterval);

            elapsedTime += damageInterval;
        }

        ChangeEmission(Color.black, colorIntensity);
        burning = false;
        yield return null;
    }

    // damage surrounding enemies
    public void SplashDamage(float damageAmount, float splashRadius) {
        int excludeLayers = LayerMask.GetMask("ground", "path", "Triggers", "Towers");
        
        // get all nearby enemies
        Collider[] nearbyEnemies = Physics.OverlapSphere(transform.position, splashRadius, ~excludeLayers);
        foreach(Collider collider in nearbyEnemies) {
            if (collider.gameObject.CompareTag("Enemy")) {
                Enemy nearby = collider.gameObject.GetComponent<Enemy>();

                // deal splash damage amount to near by enemies
                if (nearby != null) {
                    nearby.TakeDamage(damageAmount);
                }
            }
        }
    }
}
