using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmashGround : Projectile
{
    public float effectRadius;
    public float bonusDamagePerEnemy;
    public float effectTime;
    int enemyinRange = 0;
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
        bonusDamagePerEnemy *= holder.GetComponent<Champion>().currentLevel;
        Invoke("EffectEnemy", effectTime);
    }
    
    void EffectEnemy()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject t in targets)
        {
            if (Vector2.Distance(t.transform.position, transform.position) <= effectRadius)
            {
                enemyinRange++;
            }
        }

        foreach (GameObject t in targets)
        {
            if (Vector2.Distance(t.transform.position, transform.position) <= effectRadius)
            {
                t.GetComponent<Enemy>().TakeDamage(damage + bonusDamagePerEnemy * enemyinRange, holder.GetComponent<LivingEntity>());
            }
        }
    }
}
