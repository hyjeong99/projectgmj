using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySkeletonBow : Enemy
{
    public GameObject arrow;

    public GameObject arrowPoint;
    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        meshRenderer = GetComponent<MeshRenderer>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        status = new Status(UnitCode.Enemy, "원거리", 1);
        isChase = true;
        attackRange = 30.0f;
        target = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        if (navMeshAgent.enabled && Vector3.Distance(transform.position, target.position) < attackRange * 30)
        {
            navMeshAgent.SetDestination(target.position);

            navMeshAgent.isStopped = !isChase;
        }
    }

    void FixedUpdate()
    {
        bool isOverRange = Vector3.Distance(transform.position, target.position) > attackRange;
        bool isTooClose = Vector3.Distance(transform.position, target.position) < closeRange;
        Targeting(isOverRange);
        FreezeVelocity(isOverRange, isTooClose);
        transform.LookAt(target);
    }

    // 스켈레톤 궁수 공격 함수
    protected override void Targeting(bool isOverRange)
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

    protected override IEnumerator Attack()    
    {
        isChase = false;
        isAttack = true;
        animator.SetTrigger("doShoot");
        yield return new WaitForSeconds(2.0f);
        Shoot();
        yield return new WaitForSeconds(3.0f);
        isChase = true;
        isAttack = false;
    }

    void Shoot()
    {
        GameObject instantarrow = Instantiate(arrow, arrowPoint.transform.position, arrowPoint.transform.rotation);
        Rigidbody rigidarrow = instantarrow.GetComponent<Rigidbody>();
        Arrow arrowScrpit = instantarrow.GetComponent<Arrow>();
        if (arrowScrpit != null)
        {
            arrowScrpit.SetSummoner(this);
        }
        rigidarrow.velocity = transform.forward * 20;
    }
}
