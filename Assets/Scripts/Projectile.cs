using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
    
    public float speed;
    public float attackDamage = 10;
    public float lifetime = 3f;
    private float seconds;

    private void OnEnable()
    {
        seconds = lifetime;
    }

    void Update() {
        //GetComponent<Rigidbody>().AddForce(transform.forward * speed, ForceMode.Impulse);
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
        HealthController health = g.GetComponent<HealthController>();
        if (health != null) {
            health.TakeDamage(attackDamage);
            PlayerTroopSpawner.instance.AddMerits(2);
        }
    }
}
