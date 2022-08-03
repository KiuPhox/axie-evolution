using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using DG.Tweening;

public class Enemy : LivingEntity, IFollowable
{
    public Vector2 randomSpeed;
    public Vector2 randomWanderTime;
    public float detectRange;
    float speed;
    float originalSpeed;

    [HideInInspector] public float stunTime;
    float attackCooldownTime;
    float nextAttackTime;

    LivingEntity targetEntity;
    GameObject closestChampion;
    bool isStunned = false;
    float wanderTime;

    bool isWander = false;
    Vector3 desiredDirection;

    public override void Start()
    {
        base.Start();
        wanderTime = Random.Range(randomWanderTime.x, randomWanderTime.y);
        speed = Random.Range(randomSpeed.x, randomSpeed.y);
        originalSpeed = speed;
        attackCooldownTime = cooldownTime;
        playerChampions.AddBlobShadowForChampion(this.gameObject);
    }

    private void Update()
    {
        closestChampion = GetClosestTargetInList(playerChampions.champions);
        if (closestChampion != null && !isStunned)
        {
            FollowTarget(closestChampion.transform.position);
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

    public void FollowTarget(Vector3 targetPos)
    {
        Vector3 direction = targetPos - transform.position;
        if (direction.magnitude <= detectRange)
        {
            direction = targetPos - transform.position;
            transform.position += direction.normalized * speed * Time.deltaTime;
        }
        else
        {
            Vector3 offsetDirection = new Vector3(Random.Range(-1.5f, 1.5f), Random.Range(-1.5f, 1.5f), 0);
            if (Time.time > wanderTime)
            {
                wanderTime = Random.Range(randomWanderTime.x, randomWanderTime.y);
                desiredDirection = direction + offsetDirection;
            }
            transform.position += desiredDirection.normalized * speed * Time.deltaTime;
        }
    }

    public void SetSpeed(float speedEffect)
    {
        speed -= speed * speedEffect;
    }

    public void SetOriginalSpeed()
    {
        speed = originalSpeed;
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
            }
        }
    }
}
