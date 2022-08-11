using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boomeranger : Champion
{
    float closestDistance = 100f;

    private void Update()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Enemy");
        closestTarget = GetClosestTargetInList(targets);
        if (closestTarget != null)
        {
            FlipBaseOnTargetPos(closestTarget.transform.position);
            closestDistance = Vector2.Distance(transform.position, closestTarget.transform.position);
        }
        if (Time.time >= nextAttackTime)
        { 
            if (closestTarget != null && closestDistance <= championData.range && closestTarget)
            {
                nextAttackTime = Time.time + cooldownTime;
                StartCoroutine(ShootIE(0f));
                if (currentLevel == 3)
                { 
                    StartCoroutine(ShootIE(0.5f));
                    StartCoroutine(ShootIE(1f));
                }
            }
        }
    }
}
