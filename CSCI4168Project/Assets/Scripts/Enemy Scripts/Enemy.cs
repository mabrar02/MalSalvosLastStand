using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float maxHealth;
    public float deathTime=2.0f;
    public float _health;
    public GameObject floatingTextPref;

    private Animator animator;
    void Start()
    {
        _health = maxHealth;
        animator = GetComponentInChildren<Animator>();
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
}
