using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using DG.Tweening;
using TMPro;
using Spine.Unity;
using AxieMixer.Unity;
using Spine;
public class LivingEntity : MonoBehaviour, IDamageable
{
    [HideInInspector] public PlayerChampions playerChampions;
    [HideInInspector] public GameObject damagePopupHolder;

    public ChampionData championData;
    public float immortalTime = 1f;

    public int currentLevel;
    public float health;
    [HideInInspector] public float maxHealth;
    protected float damage;
    protected float defense;
    [HideInInspector] public GameObject projectile;
    protected float cooldownTime;

    [HideInInspector] public float[] multipliers;
    
    protected bool dead;

    UnitsUI unitsUI;

    TMP_Text damagePopup;

    [HideInInspector] public SkeletonAnimation skeletonAnimation;
    
    private AudioClip healClip;

    float nextImmortalTime;

    public event System.Action OnDeath;

    private void Awake()
    {
        if (skeletonAnimation == null)
        {
            skeletonAnimation = GetComponent<SkeletonAnimation>();
            Mixer.SpawnSkeletonAnimation(skeletonAnimation, championData.axieID, championData.genes);
            skeletonAnimation.state.SetAnimation(0, "draft/run-origin", true);
        }
        currentLevel = 1;
        multipliers = new float[] { 1, 1, 1, 1 };
    }

    public virtual void Start()
    {
        playerChampions = GameObject.Find("Champions Holder").GetComponent<PlayerChampions>();
        damagePopup = Resources.Load("Prefabs/Damage Popup", typeof(TMP_Text)) as TMP_Text;
        damagePopupHolder = GameObject.Find("Text Holder");
        transform.position = new Vector3(transform.position.x, transform.position.y, Random.Range(-1f, 0f));
        healClip = Resources.Load<AudioClip>("Audio/Heal");
        unitsUI = GameObject.Find("Units Holder").GetComponent<UnitsUI>();
        SetCharacteristics();
    }

    public void SetCharacteristics()
    {
        maxHealth = championData.health * currentLevel * multipliers[0];
        health = maxHealth;
        damage = championData.damage * currentLevel * multipliers[1];
        defense = championData.defense * multipliers[2];
        projectile = championData.projectile;
        cooldownTime = championData.cooldownTime * multipliers[3];
    }


    public virtual void TakeDamage(float damage, LivingEntity damagingEntity)
    {
        if (Time.time >= nextImmortalTime)
        {
            nextImmortalTime = Time.time + immortalTime;

            float incomeDamage = damage * (100 / (100 + defense));
            health -= incomeDamage;

            // Health Bar
            if (this.tag == "Champion")
            {
                unitsUI.LoadHealthbar(this.gameObject, health, maxHealth);
            }

            // Damage Popup
            if (incomeDamage > 0)
            {
                TMP_Text i_damagePopup = Instantiate(damagePopup, transform.position, Quaternion.identity, damagePopupHolder.transform);
                i_damagePopup.text = Mathf.RoundToInt(incomeDamage).ToString();
                if (damagingEntity.CompareTag("Champion"))
                {
                    i_damagePopup.color = damagingEntity.championData.cardColor.nameBoxColor;
                }
            }

            skeletonAnimation.state.SetAnimation(0, "defense/hit-by-ranged-attack", false);
            skeletonAnimation.state.AddAnimation(0, "draft/run-origin", true, 0);
        }

        if (health <= 0)
        {
            Die();
        }
    }

    public void Healing(float healAmount)
    {
        GameObject vfx_heal = Resources.Load("Prefabs/vfx_heal") as GameObject;
        Instantiate(vfx_heal, transform.position, Quaternion.identity);
        vfx_heal.GetComponent<HealEffect>().target = this.gameObject;
        health += healAmount;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
        unitsUI.LoadHealthbar(this.gameObject, health, maxHealth);
        SoundManager.Instance.PlayHealSound();
    }

    public GameObject GetClosestTargetInList(List<GameObject> targets)
    {
        GameObject closestTarget = null;
        if (targets.Count > 0)
        {
            float closestDistance = 100f;
            foreach (GameObject target in targets)
            {
                if (target.activeSelf)
                {
                    if (Vector3.Distance(target.transform.position, transform.position) < closestDistance)
                    {
                        closestDistance = Vector3.Distance(target.transform.position, transform.position);
                        closestTarget = target;
                    }
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
        OnDeath = null;
        if (championData.name == "Exploder")
        {
            Instantiate(championData.projectile, transform.position, Quaternion.identity);
        }
        gameObject.SetActive(false);
    }


    private void OnDestroy()
    {
        transform.DOKill();
    }
}
