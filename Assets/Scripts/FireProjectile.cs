using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireProjectile : MonoBehaviour {
   
    public Rigidbody bullet;

    public void Shoot() {
        GameObject obj = PlayerPooler.instance.SpawnFromPool("Bullet", transform.position + transform.forward, transform.rotation);
        obj.transform.parent = GameObject.Find("Bullets").transform;
        //Instantiate(bullet, transform.position + transform.forward, transform.rotation);
    }
}
