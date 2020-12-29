using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
    
    public float speed;
    public float attackDamage = 10;

    void Update() {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
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
