using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityMovementAI;

public class Champion : LivingEntity
{
    public float speed;
    public float spaceBetween = 0.5f;

    float nextAttackTime;

    GameObject[] targets;
    GameObject closestTarget;
    float closestDistance = 100f;

    bool isLoop = false;
    SteeringBasics steeringBasics;

    public override void Start()
    {
        base.Start();
        if (cooldownTime < 0)
        {
            isLoop = true;
            StartCoroutine(ShootIE());
        }
        else
        {
            nextAttackTime = Random.Range(Time.time, Time.time + cooldownTime);
        }
        steeringBasics = GetComponent<SteeringBasics>();
    }

    private void Update()
    {
        targets = GameObject.FindGameObjectsWithTag("Enemy");
        closestTarget = GetClosestTargetInList(targets);

        if (closestTarget != null)
        {
            FlipBaseOnTargetPos(closestTarget.transform.position);
            closestDistance = Vector2.Distance(transform.position, closestTarget.transform.position);
        }
        if (Time.time >= nextAttackTime && !isLoop)
        {
            nextAttackTime = Time.time + cooldownTime;
            if (closestTarget != null && closestDistance <= championData.range && closestTarget)
            {
                StartCoroutine(ShootIE());
                //skeletonAnimation.state.SetAnimation(0, championData.attackAnimation, false);
                //Shoot();
            }
        }
    }

    void FixedUpdate()
    {

        Vector3 targetPosition = Utility.GetMouseWorldPosition();

        FlipBaseOnTargetPos(targetPosition);

        Vector3 accel = steeringBasics.Arrive(targetPosition);

        steeringBasics.Steer(accel);

    }


    private void Shoot()
    {
        GameObject i_projectile = Instantiate(projectile, transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity, damagePopupHolder.transform);
        if (i_projectile != null)
        {
            //Please Fix
            if (closestTarget != null)
            {
                i_projectile.GetComponent<Projectile>().target = closestTarget;
            }
            i_projectile.GetComponent<Projectile>().damage = damage;
            i_projectile.GetComponent<Projectile>().holder = this.gameObject;
        }
    }
    IEnumerator ShootIE()
    {
        skeletonAnimation.state.SetAnimation(0, championData.attackAnimation, false);
        skeletonAnimation.state.AddAnimation(0, "draft/run-origin", true, 0);
        yield return new WaitForSeconds(0.4f);
        Shoot();
    }
}

