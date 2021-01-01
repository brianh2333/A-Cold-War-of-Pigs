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
    private Rigidbody rb;

    void Awake()
    {
        health = maxHealth;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
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
            if (gameObject.CompareTag("Player"))
                GameManager.instance.CIAAlive--;
            StartCoroutine(OnDeath());
        }
        
    }

    IEnumerator OnDeath()
    {
        anim.SetTrigger("Death");
        yield return new WaitForSeconds(1.4f);
        transform.Translate(Vector3.down * Time.deltaTime * 70f);
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
    }
}
