using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireProjectile : MonoBehaviour {
   
    public Rigidbody bullet;

    public void Shoot() {
        PlayerPooler.instance.SpawnFromPool("Bullet", transform.position + transform.forward, transform.rotation);
    }
}
