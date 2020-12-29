using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TroopController : MonoBehaviour {
    
    public float speed = 2;
    public float chaseDist = 5;
    public float attackDist = 5;
    public float turnSpeed = 100;
    bool isDead = false;

    Animator anim;
    Transform target;
    Rigidbody body;
    public FireProjectile fireProjectile;

    public enum State {
        Idle,
        Move,
        Attack,
        Dead
    }

    public State state = State.Idle;

    void Awake () {
        body = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        target =  GameObject.FindGameObjectWithTag("Target").transform;
        fireProjectile.GetComponent<FireProjectile>();
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
        Vector3 dir  = (target.position - transform.position).normalized;
		Vector3 cross = Vector3.Cross(transform.forward, dir);
        transform.Rotate(Vector3.up * cross.y * turnSpeed * Time.deltaTime);

        float dist = Vector3.Distance(transform.position, target.position);
        if (dist > chaseDist) {
			state = State.Idle;
		}
        else if ((dist <= attackDist) && GameManager.instance.targetsRemaining != 0) {
			state = State.Attack;
			body.velocity = Vector3.zero;
            StartCoroutine(Attack());
		}
        else if (dist > attackDist && dist < chaseDist) {
            Vector3 newPos = new Vector3(dir.x * speed * Time.fixedDeltaTime, 0, dir.z * speed * Time.fixedDeltaTime);
            body.MovePosition(body.position + newPos * Time.fixedDeltaTime);
			anim.SetBool("isWalking", true);
		}
    }

    IEnumerator Attack() {
        while (state == State.Attack && !isDead) {
			anim.SetTrigger("Attack");
            fireProjectile.Shoot();
			yield return new WaitForSeconds(2f);
			float dist = Vector3.Distance(transform.position, target.position);
            if (dist > attackDist || GameManager.instance.targetsRemaining == 0) state = State.Move;
		} 
    }
}
