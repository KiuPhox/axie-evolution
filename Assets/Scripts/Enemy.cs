using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using DG.Tweening;

public class Enemy : LivingEntity, IFollowable
{
    public float speed;

    LivingEntity targetEntity;

    float attackCooldownTime;
    float nextAttackTime;
    [HideInInspector] public float stunTime;
    
    // float closestDis = 100f;
    GameObject closestChampion;
    bool isStunned = false;

    public override void Start()
    {
        base.Start();
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
        transform.position += (targetPos - transform.position).normalized * speed * Time.deltaTime;
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
