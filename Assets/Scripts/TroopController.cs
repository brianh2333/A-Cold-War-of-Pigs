using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TroopController : MonoBehaviour {
    
    public float speed = 2;
    public float chaseDist = 5;
    public float attackDist = 5;
    bool isDead = false;

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

        float dist = Vector3.Distance(transform.position, transform.position);
		if (dist > chaseDist) {
			state = State.Idle;
		}
        else if (dist < attackDist) {
			state = State.Attack;
			body.velocity = Vector3.zero;
			StartCoroutine(Attack());
            Debug.Log("Called attack coroutine");
		}
        else {
			body.velocity = dir * speed;
			anim.SetBool("isWalking", true);
            Debug.Log("is walking");
		}
    }

    IEnumerator Attack() {
        while (state == State.Attack && !isDead) {
			anim.SetTrigger("Attack");
			yield return new WaitForSeconds(2f);
			float dist = Vector3.Distance(transform.position, transform.position);
			if (dist > attackDist) state = State.Move;
		} 
    }
}
