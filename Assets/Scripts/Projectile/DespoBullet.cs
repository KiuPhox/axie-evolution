using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespoBullet : Projectile
{
    public float speed;

    private void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime, Space.Self);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<LivingEntity>().TakeDamage(damage, holder.GetComponent<LivingEntity>());
        }
    }
}
