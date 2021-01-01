using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireProjectile : MonoBehaviour {
   
    public Rigidbody bullet;
    public AudioSource gunShootSound;

    public void Shoot() {
        if (transform.root.CompareTag("Player"))
        {
            if (transform.root.name.Contains("Gunner"))
                PlayerPooler.instance.SpawnFromPool("GunnerBullet", transform.position + transform.forward, transform.rotation);
            else
                PlayerPooler.instance.SpawnFromPool("Bullet", transform.position + transform.forward, transform.rotation);

        }
        else if (transform.root.CompareTag("Target"))
        {
            if (transform.root.name.Contains("Gunner"))
                EnemyPooler.instance.SpawnFromPool("EnemyGunnerBullet", transform.position + transform.forward, transform.rotation);
            else
                EnemyPooler.instance.SpawnFromPool("EnemyBullet", transform.position + transform.forward, transform.rotation);

        }

        gunShootSound.Play();

    }
}
