using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using DG.Tweening;
public class LivingEntity : MonoBehaviour, IDamageable
{
    [HideInInspector] public PlayerChampions playerChampions;

    public ChampionData championData;
    public float startingHealth;
    public float immortalTime = 1f;

    protected float health;
    protected float damage;
    protected GameObject projectile;
    protected float cooldownTime;


    protected bool dead;
    
    FlashEffect flashEffect;
    SpriteRenderer SR;

    // Time Management
    float nextImmortalTime;

    

    public event System.Action OnDeath;
    

    public virtual void Start()
    {
        SR = GetComponent<SpriteRenderer>();
        flashEffect = GetComponent<FlashEffect>();
        playerChampions = GameObject.Find("Champions Holder").GetComponent<PlayerChampions>();
        
        SetCharacteristics();
    }

    private void SetCharacteristics()
    {
        startingHealth = health = championData.health;
        damage = championData.damage;
        projectile = championData.projectile;
        cooldownTime = championData.cooldownTime;
    }

    public virtual void TakeHit(float damage, Vector3 hitPoint, Vector3 hitDirection)
    {
        TakeDamage(damage);
    }

    public virtual void TakeDamage(float damage)
    {
        if (Time.time >= nextImmortalTime)
        {
            nextImmortalTime = Time.time + immortalTime;
            FlashOnDamaged();
            health -= damage;
        }
        if (health <= 0 && !dead)
        {
            Die();
        }
    }

    float closestDis;
    GameObject closestTarget;

    public GameObject GetClosestTargetInList(List<GameObject> targets)
    {
        if (targets.Count > 0)
        {
            closestDis = 100f;
            foreach (GameObject target in targets)
            {
                if (Vector3.Distance(target.transform.position, transform.position) < closestDis)
                {
                    closestDis = Vector3.Distance(target.transform.position, transform.position);
                    closestTarget = target;
                }
            }
            return closestTarget;
        }
        return null;
    }

    public GameObject GetClosestTargetInList(GameObject[] targets)
    {
        if (targets.Length > 0)
        {
            closestDis = 100f;
            foreach (GameObject target in targets)
            {
                if (Vector3.Distance(target.transform.position, transform.position) < closestDis)
                {
                    closestDis = Vector3.Distance(target.transform.position, transform.position);
                    closestTarget = target;
                }
            }
            return closestTarget;
        }
        return null;
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
        if (gameObject.CompareTag("Champion"))
        {
            playerChampions.RemoveChampion(gameObject);
        }
        GameObject.Destroy(gameObject);
    }

    protected void FlashOnDamaged()
    { 
        flashEffect.Flash();
    }

    private void OnDestroy()
    {
        transform.DOKill();
    }
}
