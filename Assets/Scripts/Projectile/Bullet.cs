﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Bullet: Projectile
{
    public float speed;
    public float lifeTime;
    public ParticleSystem leafEffect;

    Vector3 targetPos;
    Vector3 direction;

    LivingEntity targetEntity;

    void Start()
    {
        if (target != null) 
        {
            targetPos = target.transform.position;
            direction = targetPos - transform.position;
            RotateToDirection(direction);
            if (leafEffect != null)
            {
                ParticleSystem i_leaf = Instantiate(leafEffect, transform.position, transform.rotation);
                Destroy(i_leaf.gameObject, 0.35f);
            }
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
    }


    //Run 1 times at the time of collision between 2 objects.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Champion"))
        {
            // Deals target damage
            targetEntity = collision.gameObject.GetComponent<LivingEntity>();
            targetEntity.TakeDamage(damage, holder.GetComponent<LivingEntity>());
            Destroy(gameObject);
        }
    }
}
