using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespoProjectile : Projectile
{
    public float lifeTime;
    Vector3 targetPos;
    Vector3 direction;
    Vector3 offset = new Vector3(0, 0.5f, 0);
    void Start()
    {
        Projectile[] bullets = GetComponentsInChildren<Projectile>();
        
        if (target != null)
        {
            for (int i = 1; i < bullets.Length; i++)
            {
                bullets[i].target = target;
                bullets[i].holder = holder;
                bullets[i].damage = damage;
            }
            targetPos = target.transform.position + offset;
            direction = targetPos - transform.position;
            RotateToDirection(direction);
        }
        else
        {
            Destroy(this.gameObject);
        }
        Destroy(this.gameObject, lifeTime);
    }
}
