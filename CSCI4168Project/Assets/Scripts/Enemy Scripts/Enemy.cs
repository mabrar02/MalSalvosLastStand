using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float maxHealth;

    private float _health;
    void Start()
    {
        _health = maxHealth;
    }

    public void Damage(float damage)
    {
        _health -= damage;
        if (_health <= 0)
        {
            gameObject.SetActive(false);
        }
    }
    
}
