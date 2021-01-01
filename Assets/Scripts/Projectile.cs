using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
    
    public float speed;
    public float attackDamage = 10;
    public float lifetime = 3f;
    private float seconds;
    public GameObject target;
    Rigidbody body;

    void Awake() {
        body = GetComponent<Rigidbody>();
    }

    void OnEnable()
    {
        target = GameObject.FindGameObjectWithTag("Target");
        seconds = lifetime;
    }

    void Update() {
        Vector3 dir = target.transform.position - transform.position;
        dir = dir.normalized;
        body.velocity = dir * speed;
        transform.forward = dir;

        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        if (seconds <= 0)
            this.gameObject.SetActive(false);
        else
            seconds -= Time.deltaTime;
    }
    
    void OnTriggerEnter(Collider c) {
        HitObject(c.gameObject);
        this.gameObject.SetActive(false);
    }

    void HitObject(GameObject g) {
        if (g.CompareTag("Target") || g.CompareTag("Obstacle"))
        {
            HealthController health = g.GetComponent<HealthController>();
            if (health != null)
            {
                health.TakeDamage(attackDamage);
                PlayerTroopSpawner.instance.AddMerits(2);
            }
        }
    }
}
