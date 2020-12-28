using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TroopController : MonoBehaviour {
    
    public float speed = 2;
    public float moveDist = 5;
    public float attackDist = 5;

    Animator anim;
    Transform target;
    Rigidbody body;

    public enum State {
        Idle,
        Move,
        Attack,
        Dead
    }

    public State state = State.Idle;

    void Awake() {
        body = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Target").transform;
    }

    void Update() {
        switch (state) {
            case State.Idle:
                IdleUpdate();
                break;
            case State.Move:
                MoveUpdate();
                break;
            default:
                break;
        }
    }

    void IdleUpdate() {
        //Set animation bool to false
        //body velocity = zero
        //distance = vector3 distance
        //if dist < moveDist state is Chase
    }

    void MoveUpdate() {
        //A lot goes here
    }

    //IEnumerator Shoot() {
        //Ill attack when at a certain distance
    //}
}
