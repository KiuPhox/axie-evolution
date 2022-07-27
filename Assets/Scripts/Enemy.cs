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
    float timeActtackCount = 0f;

    float closestDis = 100f;
    GameObject closestChampion;

    public override void Start()
    {
        base.Start();
        attackCooldownTime = championData.cooldownTime;
        playerChampions.AddBlobShadowForChampion(this.gameObject);
    }

    private void Update()
    {
        timeActtackCount += Time.deltaTime;
        ChaseClosestChampion();
    }

    private void ChaseClosestChampion()
    {
        if (playerChampions.championsCount > 0)
        {
            closestDis = 100f;
            foreach (GameObject champion in playerChampions.champions)
            {
                if (Vector3.Distance(champion.transform.position, transform.position) < closestDis)
                {
                    closestDis = Vector3.Distance(champion.transform.position, transform.position);
                    closestChampion = champion;
                }
            }
            if (closestChampion != null)
            {
                FollowTarget(closestChampion.transform.position);
                FlipBaseOnTargetPos(closestChampion.transform.position);
            }
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
            if (timeActtackCount > attackCooldownTime)
            {
                timeActtackCount = 0f;
                targetEntity = collision.gameObject.GetComponent<Champion>();
                targetEntity.TakeDamage(championData.damage);
            }
        }
    }
}
