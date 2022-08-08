using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityMovementAI;

public class Champion : LivingEntity
{
    [HideInInspector] public int[] reserve = {0, 0, 0};
    [HideInInspector] public float nextAttackTime;
    [HideInInspector] public GameObject closestTarget;

    GameObject[] targets;
    
    SteeringBasics steeringBasics;

    public override void Start()
    {
        base.Start();
        nextAttackTime = Random.Range(Time.time, Time.time + cooldownTime);
        steeringBasics = GetComponent<SteeringBasics>();
    }

    void FixedUpdate()
    {
        Vector3 targetPosition = Utility.GetMouseWorldPosition();
        if (closestTarget == null)
        {
            FlipBaseOnTargetPos(targetPosition);
        }
        Vector3 accel = steeringBasics.Arrive(targetPosition);
        steeringBasics.Steer(accel);
    }

    public void BuffLevel()
    {
        skeletonAnimation.state.SetAnimation(0, "battle/get-buff", false);
        skeletonAnimation.state.AddAnimation(0, "draft/run-origin", true, 0);
        SetCharacteristics();
    }

    public void Shoot()
    {
        GameObject i_projectile = Instantiate(projectile, transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity, damagePopupHolder.transform);
        if (i_projectile != null)
        {
            if (closestTarget != null)
            {
                i_projectile.GetComponent<Projectile>().target = closestTarget;
            }
            i_projectile.GetComponent<Projectile>().damage = damage;
            i_projectile.GetComponent<Projectile>().holder = this.gameObject;
        }
    }
    public IEnumerator ShootIE(float timeDelay)
    {
        skeletonAnimation.state.SetAnimation(0, championData.attackAnimation, false);
        skeletonAnimation.state.AddAnimation(0, "draft/run-origin", true, 0);
        yield return new WaitForSeconds(timeDelay);
        yield return new WaitForSeconds(0.4f);
        Shoot();
    }


}

