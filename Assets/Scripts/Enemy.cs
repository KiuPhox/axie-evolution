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

    
    // float closestDis = 100f;
    GameObject closestChampion;

    public override void Start()
    {
        base.Start();
        attackCooldownTime = cooldownTime;
        playerChampions.AddBlobShadowForChampion(this.gameObject);
    }

    private void Update()
    {
        closestChampion = GetClosestTargetInList(playerChampions.champions);
        if (closestChampion != null)
        {
            FollowTarget(closestChampion.transform.position);
            FlipBaseOnTargetPos(closestChampion.transform.position);
        }
    }

    public void FollowTarget(Vector3 targetPos)
    {
        transform.position += (targetPos - transform.position).normalized * speed * Time.deltaTime;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Champion"))
        {
            if (Time.time > nextAttackTime)
            {
                nextAttackTime = Time.time + attackCooldownTime;
                targetEntity = collision.gameObject.GetComponent<LivingEntity>();
                targetEntity.TakeDamage(championData.damage);
            }
        }
    }
}
