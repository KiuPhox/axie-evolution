using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medusa : Projectile
{

    public float speed;
    public float lifeTime;


    Vector3 targetPos;
    Vector3 direction;
    LivingEntity targetEntity;

    float shortestDistance = Mathf.Infinity;
    GameObject closestEnemy = new GameObject();

    // Start is called before the first frame update
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

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            if (enemy.transform.position != targetPos) 
            {
                float distance = Vector3.Distance(targetPos, enemy.transform.position);

                if (distance <= shortestDistance)
                {
                    shortestDistance = distance;
                    closestEnemy = enemy;
                }
            }
        }

        targetPos = closestEnemy.transform.position;
        direction = targetPos - transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            // Deals target damage
            targetEntity = collision.gameObject.GetComponent<LivingEntity>();
            targetEntity.TakeDamage(damage, holder.GetComponent<LivingEntity>());
        }
    }

}
