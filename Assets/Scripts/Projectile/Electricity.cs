using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Electricity : Projectile
{
    public float lifeTime;
    public float effectRadius;
    public float effectTime;

    // Start is called before the first frame update
    void Start()
    {
        if (target != null)
        {
            transform.position = target.transform.position;
        }
        else
        {
            Destroy(this.gameObject);
        }
        
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject t in targets)
        {
            if (Vector2.Distance(t.transform.position, transform.position) <= effectRadius)
            {
                t.GetComponent<LivingEntity>().TakeDamage(damage);
                t.GetComponent<Enemy>().TriggerStun(effectTime);
            }
        }

        Destroy(this.gameObject, lifeTime);
    }
}
