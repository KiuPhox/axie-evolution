using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class LivingEntity : MonoBehaviour, IDamageable
{
    [HideInInspector] public PlayerChampions playerChampions;

    public ChampionData championData;
    public float startingHealth;
    public float immortalTime = 1f;

    protected float health;
    protected bool dead;

    
    FlashEffect flashEffect;
    SpriteRenderer SR;

    float timeCount = 0;

    public event System.Action OnDeath;

    public virtual void Start()
    {
        SR = GetComponent<SpriteRenderer>();
        startingHealth = health = championData.health;
        flashEffect = GetComponent<FlashEffect>();
        playerChampions = GameObject.Find("Champions Holder").GetComponent<PlayerChampions>();
    }

    public virtual void TakeHit(float damage, Vector3 hitPoint, Vector3 hitDirection)
    {
        TakeDamage(damage);
    }

    public virtual void TakeDamage(float damage)
    {
        if (timeCount > immortalTime)
        {
            timeCount = 0;
            FlashOnDamaged();
            health -= damage;
        }
        if (health <= 0 && !dead)
        {
            Die();
        }
    }

    public virtual void FlipBaseOnTargetPos(Vector3 targetPos)
    {
        if (targetPos.x > transform.position.x)
        {
            SR.flipX = true;
        }
        else
            SR.flipX = false;
    }

    [ContextMenu("Self Destruct")]
    protected void Die()
    {
        dead = true;
        if (OnDeath != null)
        {
            OnDeath();
        }
        playerChampions.RemoveChampion(gameObject);
        GameObject.Destroy(gameObject);
    }

    protected void FlashOnDamaged()
    {
        flashEffect.Flash();
    }

    private void Update()
    {
        timeCount += Time.deltaTime;
    }
}
