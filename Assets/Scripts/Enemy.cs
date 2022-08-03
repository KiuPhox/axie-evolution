using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using DG.Tweening;

public class Enemy : LivingEntity, IFollowable
{
    public Vector2 randomSpeed;
    float speed;

    [HideInInspector] public float stunTime;
    float attackCooldownTime;
    float nextAttackTime;

    LivingEntity targetEntity;
    GameObject closestChampion;
    bool isStunned = false;

    public override void Start()
    {
        base.Start();

        speed = Random.Range(randomSpeed.x, randomSpeed.y);
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
        transform.position += direction.normalized * speed * Time.deltaTime;
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
