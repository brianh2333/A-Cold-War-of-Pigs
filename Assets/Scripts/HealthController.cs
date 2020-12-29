using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    public float maxHealth = 100;
    public float health;

    public bool isPlayer, isEnemy;

    public delegate void HealthChanged(float health);
    public event HealthChanged OnHealthChanged = delegate { };

    void Awake()
    {
        health = maxHealth;
    }
    void OnEnable()
    {
        health = maxHealth;
    }


    public void TakeDamage(float damage)
    {
        health -= damage;
        health = Mathf.Clamp(health, 0, maxHealth);
        OnHealthChanged(health);
        if( health <= 0)
        {
            gameObject.SetActive(false);
            if (gameObject.CompareTag("Player"))
                GameManager.instance.CIAAlive--;
        }
    }
}
