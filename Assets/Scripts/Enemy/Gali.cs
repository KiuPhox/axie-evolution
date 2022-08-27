using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityMovementAI;

public class Gali : Enemy
{
    public float moveTime;
    Vector3 targetPos;
    float nextMoveTime;

    Vector3 prePos;
    bool isUsedEffect = false;
    public override void Start()
    {
        base.Start();
        nextMoveTime = Time.time + moveTime;
    }

    private new void Update()
    {
        if (Time.time > nextMoveTime)
        {
            nextMoveTime = Time.time + moveTime;
            targetPos = new Vector3(Random.Range(-6f, 6f), Random.Range(-3f, 3f), 0);
            while (Vector2.Distance(prePos, targetPos) < 5f)
            {
                targetPos = new Vector3(Random.Range(-6f, 6f), Random.Range(-3f, 3f), 0);
            }
            prePos = targetPos;
            isUsedEffect = false;
            FlipBaseOnTargetPos(targetPos);
            skeletonAnimation.state.SetAnimation(0, "action/move-forward", false);
            skeletonAnimation.state.AddAnimation(0, "action/idle/normal", true, 0f);
        }
        if (Vector2.Distance(transform.position, targetPos) < 0.5f && !isUsedEffect)
        {
            isUsedEffect = true;
            StartCoroutine(Attack(0f));
        }
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
            accel = steeringBasics.Arrive(targetPos);
        }
        steeringBasics.Steer(accel);
    }

    /*
    IEnumerator TeleIE(float delayTime)
    {
        smokeEffect.GetComponent<Animator>().SetTrigger("isTele");
        yield return new WaitForSeconds(delayTime);
        transform.position = targetPos;
        smokeEffect.GetComponent<Animator>().SetTrigger("isTele");
        
    }
    */
    IEnumerator Attack(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        GameObject i_slide = Instantiate(championData.projectile, transform.position + new Vector3(0f, 0.5f, 0f), Quaternion.identity);
        Destroy(i_slide, 1f);
    }
    
}
