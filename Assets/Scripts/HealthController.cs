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

    public Animator anim;
    private bool canDie;

    void Awake()
    {
        health = maxHealth;
        anim = GetComponent<Animator>();
    }
    void OnEnable()
    {
        health = maxHealth;
        canDie = true;
    }


    public void TakeDamage(float damage)
    {
        health -= damage;
        health = Mathf.Clamp(health, 0, maxHealth);
        OnHealthChanged(damage);
      
        if( health <= 0 && canDie)
        {
            canDie = false;
            StartCoroutine(OnDeath());
            if (gameObject.CompareTag("Player"))
                GameManager.instance.CIAAlive--;
        }
        
    }

    IEnumerator OnDeath()
    {
        anim.SetTrigger("Death");
        yield return new WaitForSeconds(1.5f);
        gameObject.SetActive(false);
        GameManager.instance.CIAAlive--;
    }
}
