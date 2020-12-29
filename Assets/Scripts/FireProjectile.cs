using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireProjectile : MonoBehaviour {
   
    public Rigidbody bullet;

    public void Shoot() {
        Instantiate(bullet, transform.position + transform.forward, transform.rotation);
    }
}
