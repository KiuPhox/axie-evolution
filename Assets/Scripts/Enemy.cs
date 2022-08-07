using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using DG.Tweening;
using UnityMovementAI;


public class Enemy : LivingEntity
{
    [HideInInspector] public float stunTime;
    float attackCooldownTime;
    float nextAttackTime;

    LivingEntity targetEntity;
    GameObject closestChampion;
    

    SteeringBasics steeringBasics;
    Wander1 wander;
    Separation separation;

    List<MovementAIRigidbody> otherEnemies;

    public float separationWeight;
    public float arriveWeight;

    bool isStunned = false;
    Vector3 accel = new Vector3(0, 0, 0);

    public override void Start()
    {
        base.Start();

        attackCooldownTime = cooldownTime;
        steeringBasics = GetComponent<SteeringBasics>();
        wander = GetComponent<Wander1>();
        separation = GetComponent<Separation>();
    }

    private void Update()
    {
        closestChampion = GetClosestTargetInList(playerChampions.champions);
        if (closestChampion != null && !isStunned)
        {
            FlipBaseOnTargetPos(closestChampion.transform.position);
        }
        if (stunTime > 0)
        {
            isStunned = true;
            stunTime -= Time.deltaTime;
        }
        else
        {
            isStunned = false;
        }
    }

    private void FixedUpdate()
    {
        otherEnemies = new List<MovementAIRigidbody>();

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject e in enemies)
        {
            if (e != this.gameObject)
            {
                otherEnemies.Add(e.GetComponent<MovementAIRigidbody>());
            }
        }

        if (!isStunned)
        {
            // Basic Movement
            accel = wander.GetSteering();
            accel += separation.GetSteering(otherEnemies) * separationWeight;
            if (closestChampion != null && !isStunned)
            {
                accel += steeringBasics.Arrive(closestChampion.transform.position) * arriveWeight;
            }
        }

        else if (isStunned)
        {
            accel = steeringBasics.Arrive(transform.position);
        }
        steeringBasics.Steer(accel);
    }

    public void ResetAllEffect()
    {
        stunTime = 0;
        skeletonAnimation.state.SetAnimation(0, "draft/run-origin", true);
    }

    public void SetEffectSpeed(float effectSpeed)
    {
        arriveWeight -= effectSpeed;
    }

    public void SetOriginalSpeed()
    {
        arriveWeight = 1;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Champion") && !isStunned)
        {
            if (Time.time > nextAttackTime)
            {
                nextAttackTime = Time.time + attackCooldownTime;
                targetEntity = collision.gameObject.GetComponent<LivingEntity>();
                targetEntity.TakeDamage(championData.damage, GetComponent<LivingEntity>());
                StartCoroutine(AttackIE());
            }
        }
    }

    IEnumerator AttackIE()
    {
        skeletonAnimation.state.SetAnimation(0, championData.attackAnimation, false);
        skeletonAnimation.state.AddAnimation(0, "draft/run-origin", true, 0);
        yield return new WaitForSeconds(0.5f);
    }
}