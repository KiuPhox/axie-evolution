using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSplash : Projectile
{
    private void Start()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject t in targets)
        {
            if (Vector2.Distance(t.transform.position, transform.position) <= 8f)
            {
                t.GetComponent<Enemy>().TakeDamage(damage, holder.GetComponent<LivingEntity>());
                if (holder.GetComponent<Champion>().currentLevel == 3)
                {
                    t.GetComponent<Enemy>().chuggerPushed = true;
                }
            }
        }
        Destroy(gameObject, 1.1f);
    }
}
