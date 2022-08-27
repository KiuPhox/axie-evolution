using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityMovementAI;

public class Skud : Enemy
{
    public float moveTime;
    Vector3 targetPos;
    float nextMoveTime;

    bool isUsedEffect = false;

    public override void Start()
    {
        base.Start();
        nextMoveTime = Time.time + moveTime;
    }

    List<Vector3> positions;

    private new void Update()
    {
        if (Time.time > nextMoveTime)
        {
            nextMoveTime = Time.time + moveTime;
            targetPos = new Vector3(Random.Range(-6f, 6f), Random.Range(-3f, 3f), 0);
            FlipBaseOnTargetPos(targetPos);
            isUsedEffect = false;
            skeletonAnimation.state.SetAnimation(0, "action/move-forward", true);
        }
        if (Vector2.Distance(transform.position, targetPos) < 0.5f && !isUsedEffect)
        {
            isUsedEffect = true;
            
            skeletonAnimation.state.SetAnimation(0, "attack/ranged/cast-high", false);
            skeletonAnimation.state.AddAnimation(0, "action/idle/normal", true, 0);
            positions = new List<Vector3>();
            StartCoroutine(DropExplosion(1f));
            StartCoroutine(DropExplosion(1.1f));
            StartCoroutine(DropExplosion(1.2f));
            StartCoroutine(DropExplosion(1.3f));
        }
    }

    IEnumerator DropExplosion(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);

        Vector3 currentPos = new Vector3(Random.Range(-6f, 6f), Random.Range(-3f, 3f), 0);
        
        foreach (Vector3 pos in positions)
        {
            while (Vector2.Distance(pos, currentPos) < 4f || Vector2.Distance(currentPos, transform.position) < 4f)
            {
                currentPos = new Vector3(Random.Range(-6f, 6f), Random.Range(-3f, 3f), 0);
            }
        }
        
        positions.Add(currentPos);
        GameObject i_projectile = Instantiate(championData.projectile, currentPos, Quaternion.identity);
        i_projectile.GetComponent<Projectile>().damage = damage;
    }

    private void FixedUpdate()
    {
        otherEnemies = new List<MovementAIRigidbody>();

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject e in enemies)
        {
            if (e != this.gameObject)
            {
                otherEnemies.Add(e.GetComponent<MovementAIRigidbody>());
            }
        }

        if (!isStunned)
        {
            // Basic Movement
            accel = steeringBasics.Arrive(targetPos);
        }
        steeringBasics.Steer(accel);
    }
}
