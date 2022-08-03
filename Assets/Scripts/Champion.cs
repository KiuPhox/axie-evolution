using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Champion : LivingEntity, IFollowable
{
    public float speed;
    public float spaceBetween = 0.5f;

    float nextAttackTime;

    GameObject[] targets;
    GameObject closestTarget;
    float closestDistance = 100f;

    bool isLoop = false;
    public override void Start()
    {
        base.Start();
        if (cooldownTime < 0)
        {
            isLoop = true;
            Shoot();
        }
        else
        {
            nextAttackTime = Random.Range(Time.time, Time.time + cooldownTime);
        }
    }

    private void Update()
    {
        targets = GameObject.FindGameObjectsWithTag("Enemy");
        closestTarget = GetClosestTargetInList(targets);

        if (closestTarget != null)
        {
            closestDistance = Vector2.Distance(transform.position, closestTarget.transform.position);
        }
        else if (Time.time >= nextAttackTime && !isLoop)
        {    
            nextAttackTime = Time.time + cooldownTime;
            if (closestDistance <= championData.range)
            {
                Shoot();
            }
        }
    }

    void FixedUpdate()
    {
        
        Vector3 targetPos = Utility.GetMouseWorldPosition();

        FlipBaseOnTargetPos(targetPos);

        if (GameManager.Instance.State == GameState.GameStart)
        {
            if (Vector3.Distance(targetPos, transform.position) > spaceBetween)
            {
                FollowTarget(targetPos);
            }
        }
    }

    public void FollowTarget(Vector3 targetPos)
    {
        transform.position += (targetPos - transform.position).normalized * speed * Time.fixedDeltaTime;
    }

    

    private void Shoot()
    {
        GameObject i_projectile = Instantiate(projectile, transform.position, Quaternion.identity, damagePopupHolder.transform);
        if (i_projectile != null) {
            //Please Fix
            if (closestTarget != null)
            {
                i_projectile.GetComponent<Projectile>().target = closestTarget;
            }
            i_projectile.GetComponent<Projectile>().damage = damage;
            i_projectile.GetComponent<Projectile>().holder = this.gameObject;
        }
    }
}
