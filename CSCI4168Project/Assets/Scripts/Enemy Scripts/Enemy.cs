using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float maxHealth;

    public float _health;
    public GameObject floatingTextPref;
    void Start()
    {
        _health = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        _health -= damage;
        if (_health <= 0)
        {
            Destroy(gameObject);
        }
        else {
            ShowFloatingText();
        }
    }

    private void ShowFloatingText() {
        var text = Instantiate(floatingTextPref, transform.position, Quaternion.identity, transform);
        text.GetComponent<TextMesh>().text = _health.ToString();

    }
}
