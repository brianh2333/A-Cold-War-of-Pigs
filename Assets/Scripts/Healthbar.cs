using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    public Slider slider;
    public HealthController healthController;

    void Awake()
    {
        slider = GetComponentInChildren<Slider>();
        healthController = GetComponentInParent<HealthController>();
        slider.maxValue = healthController.maxHealth;
        slider.value = healthController.maxHealth;
    }

    void OnEnable()
    {
        healthController.OnHealthChanged += ChangeHealth;
        slider.value = healthController.maxHealth;
        //StartCoroutine(Test(40f));
    }

    void OnDisable()
    {
        healthController.OnHealthChanged -= ChangeHealth;
    }


    void ChangeHealth(float health)
    {
        slider.value -= health;
    }

    //testing healthbar
    /*
    IEnumerator Test(float damage)
    {
        while( healthController.health > 0)
        {
            yield return new WaitForSeconds(2f);
            ChangeHealth(damage);
            healthController.health -= damage;
            if (healthController.health <= 0)
            {
                Debug.Log("dead");
                gameObject.SetActive(false);
            }
        }
    }
    */
}
