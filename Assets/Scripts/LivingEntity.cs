using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class LivingEntity : MonoBehaviour, IDamageable
{
    public float startingHealth;
    public float immortalTime = 1f;

    protected float health;
    protected bool dead;

    PlayerChampions playerChampions;
    FlashEffect flashEffect;

    float timeCount = 0;


    public event System.Action OnDeath;

    protected virtual void Start()
    {
        health = startingHealth;
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
            flashEffect.Flash();
            health -= damage;
        }
        if (health <= 0 && !dead)
        {
            Die();
        }
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

    }

    private void Update()
    {
        timeCount += Time.deltaTime;
    }
}
