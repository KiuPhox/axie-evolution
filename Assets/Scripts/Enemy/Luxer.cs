using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Luxer : Enemy
{
    public float moveTime;
    Vector3 targetPos;
    float nextMoveTime;

    bool isUsedEffect = false;
    public GameObject smokeEffect;

    public override void Start()
    {
        base.Start();
        nextMoveTime = Time.time + moveTime;
        skeletonAnimation.state.SetAnimation(0, "action/idle/normal", true);
    }

    List<Vector3> positions;

    private new void Update()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        if (Time.time > nextMoveTime)
        {
            nextMoveTime = Time.time + moveTime;
            targetPos = new Vector3(Random.Range(-6f, 6f), Random.Range(-3f, 3f), 0);
            StartCoroutine(TeleIE(0.2f));
            isUsedEffect = false;
            skeletonAnimation.state.SetAnimation(0, "action/idle/normal", true);
        }
        
        if (!isUsedEffect)
        {
            isUsedEffect = true;
            skeletonAnimation.state.SetAnimation(0, "action/random-02", false);
            StartCoroutine(Attack(0.8f));
        }
        
    }

    private void FixedUpdate()
    {

    }

    IEnumerator TeleIE(float delayTime)
    {
        smokeEffect.GetComponent<Animator>().SetTrigger("isTele");
        yield return new WaitForSeconds(delayTime);
        transform.position = targetPos;
        smokeEffect.GetComponent<Animator>().SetTrigger("isTele");
        
    }

    IEnumerator Attack(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        GameObject i_slide = Instantiate(championData.projectile, transform.position + new Vector3(0f, 3f, 0f), Quaternion.identity);
        Destroy(i_slide, 1f);
    }
}
