using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TroopController : MonoBehaviour {
    
    public float speed = 2;
    public float chaseDist = 5;
    public float attackDist = 5;
    //public float turnSpeed = 5;

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
        target =  GameObject.FindGameObjectWithTag("Target").transform;
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
        Debug.Log("Idle update");
        anim.SetBool("isWalking", false);
		body.velocity = Vector3.zero;
		float dist = Vector3.Distance(transform.position, target.position);
		if (dist < chaseDist) {
			state = State.Move;
		}
    }

    void MoveUpdate() {
        Debug.Log("Move update");
        Vector3 dir  = (target.position - transform.position).normalized;
		Vector3 cross = Vector3.Cross(transform.forward, dir);
		transform.Rotate(Vector3.up * cross.y * Time.deltaTime);

        float dist = Vector3.Distance(transform.position, transform.position);
		if (dist > chaseDist) {
			state = State.Idle;
		}
        else if (dist < attackDist) {
			state = State.Attack;
			body.velocity = Vector3.zero;
			//StartCoroutine(Attack());
		}
        else {
			body.velocity = dir * speed;
			anim.SetBool("isWalking", true);
		}
    }

    //IEnumerator Shoot() {
        //Ill attack when at a certain distance
    //}
}
