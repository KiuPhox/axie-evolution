using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Projectile
{
    public float speed;
    public float lifeTime;

    [HideInInspector] public GameObject target;
    Vector3 targetPos;
    Vector3 direction;
    float angle;

    LivingEntity targetEntity;

    void Start()
    {
        if (target != null)
        {
            targetPos = target.transform.position;
            direction = targetPos - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle - 90);
        }
        Destroy(this.gameObject, lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            targetEntity = collision.gameObject.GetComponent<LivingEntity>();
            targetEntity.TakeDamage(damage);
            Destroy(this.gameObject);
        }
    }
}
