﻿using System.Collections;
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
            if (gameObject.CompareTag("Player"))
                GameManager.instance.CIAAlive--;
            if (!gameObject.CompareTag("Obstacle"))
                StartCoroutine(OnDeath());
            else 
                gameObject.SetActive(false);
        }
        
    }

    IEnumerator OnDeath()
    {
        string tag = gameObject.tag;
        gameObject.tag = "Untagged";
        anim.SetTrigger("Death");
        if (tag == "Player") {
            yield return new WaitForSeconds(1.5f);
            transform.Translate(Vector3.down * Time.deltaTime * 50f);
        }
        else
        {
            if ( !(transform.name.Contains("Gunner")) )
            {
                yield return new WaitForSeconds(.5f);
                transform.Translate(Vector3.down * Time.deltaTime * 50f);
            }
            else
                gameObject.SetActive(false);
            //yield return new WaitForSeconds(2f);
        }
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
    }
}
