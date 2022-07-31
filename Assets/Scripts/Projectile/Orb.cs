using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orb : Projectile
{
    public float speed;
    public float lifeTime;
    public float effectTime;
    public float effectRadius;

    Vector3 targetPos;
    Vector3 direction;

    LivingEntity targetEntity;
    float nextEffectTime;

    void Start()
    {
        if (target != null)
        {
            targetPos = target.transform.position;
            direction = targetPos - transform.position;
        }
        else
        {
            Destroy(this.gameObject);
        }
        Destroy(this.gameObject, lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);

        GameObject[] targets = GameObject.FindGameObjectsWithTag("Enemy");

        if (Time.time >= nextEffectTime)
        {
            nextEffectTime = Time.time + effectTime;
            foreach (GameObject t in targets)
            {
                if (Vector2.Distance(t.transform.position, transform.position) <= effectRadius)
                {
                    t.GetComponent<LivingEntity>().TakeDamage(damage);
                }
            }
        }
    }
}
