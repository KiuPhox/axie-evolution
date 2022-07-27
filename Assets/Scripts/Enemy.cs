using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Enemy : MonoBehaviour, IFollowable
{
    public ChampionData enemyData;
    PlayerChampions playerChampions;
    public float speed;
    SpriteRenderer enemySR;

    LivingEntity targetEntity;

    float attackCooldownTime;
    float timeCount = 0f;

    float closestDis = 100f;
    GameObject closestChampion;

    void Start()
    {
        enemySR = GetComponent<SpriteRenderer>();
        playerChampions = GameObject.Find("Champions Holder").GetComponent<PlayerChampions>();
        attackCooldownTime = enemyData.cooldownTime;
    }

    private void Update()
    {
        timeCount += Time.deltaTime;
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

    void FlipBaseOnTargetPos(Vector3 targetPos)
    {
        if (targetPos.x > transform.position.x)
        {
            enemySR.flipX = true;
        }
        else
            enemySR.flipX = false;
    }


    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Champion"))
        {
            if (timeCount > attackCooldownTime)
            {
                timeCount = 0f;
                targetEntity = collision.GetComponent<LivingEntity>();
                targetEntity.TakeDamage(enemyData.damage);
            }

        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Champion"))
        {
            if (timeCount > attackCooldownTime)
            {
                timeCount = 0f;
                targetEntity = collision.gameObject.GetComponent<Champion>();
                targetEntity.TakeDamage(enemyData.damage);
            }

        }
    }
}
