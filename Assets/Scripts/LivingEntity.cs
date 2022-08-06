using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using DG.Tweening;
using TMPro;
using Spine.Unity;
using AxieMixer.Unity;
public class LivingEntity : MonoBehaviour, IDamageable
{
    [HideInInspector] public PlayerChampions playerChampions;
    [HideInInspector] public GameObject damagePopupHolder;

    public ChampionData championData;
    public float immortalTime = 1f;

    public float health;
    protected float damage;
    protected GameObject projectile;
    protected float cooldownTime;


    protected bool dead;
    
    FlashEffect flashEffect;

    TMP_Text damagePopup;

    SkeletonAnimation skeletonAnimation;

    // Time Management
    float nextImmortalTime;


    public event System.Action OnDeath;

    private void Awake()
    {
        if (skeletonAnimation == null)
        {
            skeletonAnimation = GetComponent<SkeletonAnimation>();
            Mixer.SpawnSkeletonAnimation(skeletonAnimation, championData.axieID, championData.genes);
        }
    }
    public virtual void Start()
    { 
        playerChampions = GameObject.Find("Champions Holder").GetComponent<PlayerChampions>();
        damagePopup = Resources.Load("Prefabs/Damage Popup", typeof(TMP_Text)) as TMP_Text;
        damagePopupHolder = GameObject.Find("Text Holder");
        SetCharacteristics();
    }

    public void SetCharacteristics()
    {
        Debug.Log(championData.health);
        health = championData.health;
        damage = championData.damage;
        projectile = championData.projectile;
        cooldownTime = championData.cooldownTime;
    }

    public virtual void TakeDamage(float damage, LivingEntity damagingEntity)
    {
        if (Time.time >= nextImmortalTime)
        {
            nextImmortalTime = Time.time + immortalTime;
            FlashOnDamaged();
            // Debug.Log(incomeDamage)));

            float incomeDamage = damage * (100 / (100 + championData.defense));
            health -= incomeDamage;

            if (incomeDamage > 0)
            {
                TMP_Text i_damagePopup = Instantiate(damagePopup, transform.position, Quaternion.identity, damagePopupHolder.transform);
                i_damagePopup.text = Mathf.RoundToInt(incomeDamage).ToString();
                if (damagingEntity.CompareTag("Champion"))
                {
                    i_damagePopup.color = damagingEntity.championData.cardColor.nameBoxColor;
                }
            }
        }

        if (health <= 0)
        {
            Die();
        }
    }

    public GameObject GetClosestTargetInList(List<GameObject> targets)
    {
        GameObject closestTarget = null;
        if (targets.Count > 0)
        {
            float closestDistance = 100f;
            foreach (GameObject target in targets)
            {
                if (Vector3.Distance(target.transform.position, transform.position) < closestDistance)
                {
                    closestDistance = Vector3.Distance(target.transform.position, transform.position);
                    closestTarget = target;
                }
            }
            return closestTarget;
        }
        return null;
    }

    public GameObject GetClosestTargetInList(GameObject[] targets)
    {
        GameObject closestTarget = null;
        if (targets.Length > 0)
        {
            float closestDistance = 100f;
            foreach (GameObject target in targets)
            {
                if (Vector3.Distance(target.transform.position, transform.position) < closestDistance)
                {
                    closestDistance = Vector3.Distance(target.transform.position, transform.position);
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
            skeletonAnimation.skeleton.ScaleX = -1;
        }
        else
        {
            skeletonAnimation.skeleton.ScaleX = 1;
        }
           
    }

    [ContextMenu("Self Destruct")]
    protected void Die()
    {
        if (OnDeath != null)
        {
            OnDeath();
        }
        if (gameObject.CompareTag("Champion"))
        {
            playerChampions.RemoveChampion(gameObject);
            GameObject.Destroy(gameObject, 0.12f);
        }
        else
        {
            OnDeath = null;
            gameObject.SetActive(false);
        }
    }

    protected void FlashOnDamaged()
    { 
        
    }

    private void OnDestroy()
    {
        transform.DOKill();
    }
}
