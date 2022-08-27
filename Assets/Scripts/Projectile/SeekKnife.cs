using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityMovementAI;

public class SeekKnife : Projectile
{
    SteeringBasics steeringBasics;
    public float seekAcceleration;
    public Vector2 randomTimeSeek;
    public float lifeTime;
    public AudioClip knifeSlice;
    
    
    Vector3 accel;
    float timeSeek;

    GameObject closestTarget;
    // Start is called before the first frame update
    void Start()
    {
        timeSeek = Random.Range(randomTimeSeek.x, randomTimeSeek.y) + Time.time;
        lifeTime += Time.time;
        steeringBasics = GetComponent<SteeringBasics>();
        accel = new Vector3(0, 0f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Enemy");

        if (targets.Length > 0)
        {
            closestTarget = targets[Random.Range(0, targets.Length - 1)];
        }

        if (Time.time > timeSeek)
        {
            if (closestTarget != null)
            {
                accel = steeringBasics.Seek(closestTarget.transform.position + new Vector3(0, 0.5f, 0), seekAcceleration);
            }
        }
        else
        {
            accel = steeringBasics.Arrive(transform.position * 2);
        }

        if (Time.time > lifeTime)
        {
            Destroy(this.gameObject);
        }

        steeringBasics.Steer(accel);
        steeringBasics.LookWhereYoureGoing();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            if (holder == null)
            {
                collision.GetComponent<LivingEntity>().TakeDamage(damage, null);
            }
            else 
                collision.GetComponent<LivingEntity>().TakeDamage(damage, holder.GetComponent<LivingEntity>());
            SoundManager.Instance.PlaySound(knifeSlice);
        }
    }
}
