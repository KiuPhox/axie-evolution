using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using DG.Tweening;
using TMPro;
public class LivingEntity : MonoBehaviour, IDamageable
{
    [HideInInspector] public PlayerChampions playerChampions;
    [HideInInspector] public GameObject damagePopupHolder;

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

    TMP_Text damagePopup;
    

    // Time Management
    float nextImmortalTime;


    public event System.Action OnDeath;
    

    public virtual void Start()
    {
        SR = GetComponent<SpriteRenderer>();
        flashEffect = GetComponent<FlashEffect>();
        playerChampions = GameObject.Find("Champions Holder").GetComponent<PlayerChampions>();
        damagePopup = Resources.Load("Prefabs/Damage Popup", typeof(TMP_Text)) as TMP_Text;
        damagePopupHolder = GameObject.Find("Text Holder");
        SetCharacteristics();
    }

    private void SetCharacteristics()
    {
        startingHealth = health = championData.health;
        damage = championData.damage;
        projectile = championData.projectile;
        cooldownTime = championData.cooldownTime;
    }

    public virtual void TakeDamage(float damage)
    {
        if (Time.time >= nextImmortalTime)
        {
            nextImmortalTime = Time.time + immortalTime;
            FlashOnDamaged();
            // Debug.Log(damage * (100 / (100 + championData.defense)));

            float incomeDamage = damage * (100 / (100 + championData.defense));
            health -= incomeDamage;

            if (incomeDamage > 0)
            {
                TMP_Text i_damagePopup = Instantiate(damagePopup, transform.position, Quaternion.identity, damagePopupHolder.transform);
                i_damagePopup.text = Mathf.RoundToInt(incomeDamage).ToString();
            }
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
        GameObject.Destroy(gameObject, 0.12f);
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
