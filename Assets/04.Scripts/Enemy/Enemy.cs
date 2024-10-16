using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public Transform target;
    public Item item;
    public Status status;

    protected bool isChase;
    protected bool isMove;
    protected bool isAttack = false;
    protected bool isDead;
    protected bool isHit = false;

    protected int hitcount = 0;

    protected float attackRange = 1.5f;
    protected float closeRange = 0.5f;

    protected Rigidbody rigidBody;
    protected CapsuleCollider capsuleCollider;
    protected MeshRenderer meshRenderer;
    protected NavMeshAgent navMeshAgent;
    protected Animator animator;

    Boss boss;

    private void Awake()
    {
        //rigidbody = GetComponent<Rigidbody>();
        //boxCollider = GetComponent<BoxCollider>();
        //meshRenderer = GetComponent<MeshRenderer>();
        //navMeshAgent = GetComponent<NavMeshAgent>();
        //animator = GetComponent<Animator>();
        //isMove = true;
        //isChase = true;
        //animator.SetBool("isMove", true);
    }

    private void Update()
    {
        //float distance = Vector3.Distance(tr)
        OnDie();
    }

    private void FixedUpdate()
    {
        bool isOverRange = Vector3.Distance(transform.position, target.position) > attackRange;
        bool isTooClose = Vector3.Distance(transform.position, target.position) < closeRange;
        Targeting(isOverRange);
        FreezeVelocity(isOverRange, isTooClose);
    }

    protected void FreezeVelocity(bool isOverRange, bool isTooClose)
    {
        if(isChase && !isOverRange || isTooClose)
        {
            rigidBody.velocity = Vector3.zero;
            rigidBody.angularVelocity = Vector3.zero;
        }
    }

    protected virtual void Targeting(bool isOverRange)
    {
        if (isOverRange && !isAttack)
        {
            return;
        }
        if (isAttack)
        {
            return;
        }
        StartCoroutine(Attack());
    }

    protected virtual IEnumerator Attack()
    {
        isChase = false;
        isMove = false;
        animator.SetBool("isMove", false);
        isAttack = true;
        animator.SetTrigger("doAttack");
        yield return new WaitForSeconds(1.8f);
        isChase = true;
        isMove = true;
        animator.SetBool("isMove", true);
        isAttack = false;
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Weapon")
        {
            StartCoroutine(Hit(1));
        }
        else if (other.gameObject.tag == "Weapon2")
        {
            StartCoroutine(Hit(2));
        }
    }

    protected IEnumerator Hit(int hitnum)
    {
        if (hitnum == 1)
        {
            animator.SetTrigger("doHit");
        }
        else
        {
            animator.SetTrigger("doAirborne");
        }
        hitcount++;
        if (hitcount > 5) OnDie();
        else yield return new WaitForSeconds(0.5f);
    }

    protected virtual void OnDie()
    {
        // �ӽ� �ּ� -------- if (status.CurrentHP > 0) return;
        Vector3 position = new Vector3 (transform.position.x, transform.position.y + 1, transform.position.z);
        Instantiate(item, position, Quaternion.identity);
        Destroy(gameObject);
    }
}
