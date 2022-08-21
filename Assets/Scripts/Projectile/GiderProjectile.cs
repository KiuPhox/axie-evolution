using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiderProjectile : Projectile
{
    public float explodeRange;
    public GameObject explodeHit;
    public float lifeTime;
    public float stunTime;

    private bool isStun = false;


    private void Start()
    {
        lifeTime += Time.time;
        if(holder.GetComponent<Champion>().currentLevel == 3)
        {
            isStun = true;
        }
    }

    private void Update()
    {
        if (Time.time >= lifeTime)
        {
            TakeDamageEnemyNearby();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            TakeDamageEnemyNearby();
        }
    }

    void TakeDamageEnemyNearby()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject t in targets)
        {
            if (Vector2.Distance(transform.position, t.transform.position) <= explodeRange)
            {
                t.GetComponent<LivingEntity>().TakeDamage(damage, holder.GetComponent<LivingEntity>());
                if (isStun)
                {
                    t.GetComponent<Enemy>().stunTime = stunTime;
                }
            }
        }
        Instantiate(explodeHit, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
