using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Boomerang : Projectile
{
    public float speed;
    public float lifeTime;
    public float effectTime;
    public float maxDistance;


    bool isBack = false;

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
            RotateToDirection(direction);
        }
        else
        {
            Destroy(this.gameObject);
        }
        Destroy(this.gameObject, lifeTime);
    }
    // đó nó mát tiep

    void Update()
    {
        transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);
        
        if (Time.time >= effectTime && !isBack)
        {
            isBack = true;

            
            targetPos = target.transform.position;
            direction = transform.position - targetPos;
            RotateToDirection(direction);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if (collision.CompareTag("Enemy"))
        {
            // Deals target damage
            targetEntity = collision.gameObject.GetComponent<LivingEntity>();
            targetEntity.TakeDamage(damage);
        }
    }
}
