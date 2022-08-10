using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nozzle : Champion
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
            nextAttackTime = Time.time + cooldownTime;
            if (closestTarget != null && closestDistance <= championData.range && closestTarget)
            {
                StartCoroutine(ShootIE(0));
                if (currentLevel == 3)
                {
                    StartCoroutine(ShootIE(1f));
                }
            }
        }
    }
}
