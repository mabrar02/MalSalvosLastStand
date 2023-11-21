using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{

    public float maxHealth;
    public float deathTime=2.0f;
    public float _health;
    public GameObject floatingTextPref;
    public NavMeshAgent _agent;
    public float agentSpeedOriginal;

    private bool slowing;
    private bool burning;

    private Animator animator;
    void Start()
    {
        _health = maxHealth;
        animator = GetComponentInChildren<Animator>();
        _agent = GetComponent<NavMeshAgent>();

        slowing = false;
        burning = false;
    }

    public void TakeDamage(float damage)
    {
        _health -= damage;
        if (_health <= 0)
        {
            die();
        }
        else {
            ShowFloatingText();
        }

        void die()
        {
            Destroy(gameObject, deathTime);
            animator.SetTrigger("Die");
        }
    }

    private void ShowFloatingText() {
        var text = Instantiate(floatingTextPref, transform.position, Quaternion.identity, transform);
        text.GetComponent<TextMesh>().text = _health.ToString();

    }
    public void ChangeEnemySpeedCoroutine(float speedReduction, float duration) {
        if(!slowing) StartCoroutine(ChangeEnemySpeed(speedReduction, duration));
    }

    public void DamageOverTimeCoroutine(float damageAmount, float duration, float damageInterval) {
        if (!burning) StartCoroutine(DamageOverTime(damageAmount, duration, damageInterval));
    }

    public IEnumerator ChangeEnemySpeed(float speedReduction, float duration) {
        slowing = true;
        if (_agent != null) {
            _agent.speed -= speedReduction;
            if(_agent.speed <= 1) {
                _agent.speed = 1;
            }

            yield return new WaitForSeconds(duration);

            _agent.speed = agentSpeedOriginal;

        }

        slowing = false;
        yield return null;
    }

    public IEnumerator DamageOverTime(float damageAmount, float duration, float damageInterval) {
        burning = true;
        float elapsedTime = 0f;
        while(elapsedTime < duration) {
            TakeDamage(damageAmount);

            yield return new WaitForSeconds(damageInterval);

            elapsedTime += damageInterval;
        }

        burning = false;
        yield return null;
    }

    public void SplashDamage(float damageAmount, float splashRadius) {
        int excludeLayers = LayerMask.GetMask("ground", "path", "Triggers", "Towers");
        Collider[] nearbyEnemies = Physics.OverlapSphere(transform.position, splashRadius, ~excludeLayers);
        foreach(Collider collider in nearbyEnemies) {
            if (collider.gameObject.CompareTag("Enemy")) {
                Enemy nearby = collider.gameObject.GetComponent<Enemy>();
                if (nearby != null) {
                    nearby.TakeDamage(damageAmount);
                }
            }
        }
    }
}
