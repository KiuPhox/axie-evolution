using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using DG.Tweening;
using TMPro;
using Spine.Unity;
using AxieCore.AxieMixer;
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
    public float cooldownTime;
    float cooldownTime_o;
    [HideInInspector] public bool isEnchanted = false;
    protected bool dead;

    UnitsUI unitsUI;

    TMP_Text damagePopup;

    [HideInInspector] public SkeletonAnimation skeletonAnimation;

    float nextImmortalTime;

    public event System.Action OnDeath;

    public virtual void Awake()
    {
        if (skeletonAnimation == null)
        {
            skeletonAnimation = GetComponent<SkeletonAnimation>();
            if (championData.classValue != 0)
            {
                BuildAxie();
            }
            else
            {
                Mixer.SpawnSkeletonAnimation(skeletonAnimation, championData.axieID, championData.genes);
            }
            skeletonAnimation.state.SetAnimation(0, "draft/run-origin", true);
        }
        currentLevel = 1;
        playerChampions = GameObject.Find("Champions Holder").GetComponent<PlayerChampions>();
    }

    public virtual void Start()
    {
        damagePopup = Resources.Load("Prefabs/Damage Popup", typeof(TMP_Text)) as TMP_Text;
        damagePopupHolder = GameObject.Find("Text Holder");
        transform.position = new Vector3(transform.position.x, transform.position.y, Random.Range(-1f, 0f));
        unitsUI = GameObject.Find("Units Holder").GetComponent<UnitsUI>();
        if (gameObject.CompareTag("Champion"))
        {
            SetCharacteristics();
        }
    }

    public void SetCharacteristics()
    {
        maxHealth = championData.health * currentLevel * playerChampions.plantHealth_m * playerChampions.lastStand_m;
        health = maxHealth;
        damage = championData.damage * currentLevel * playerChampions.squirl_m[0] * playerChampions.lastStand_m;
        defense = championData.defense * playerChampions.squirl_m[1] * playerChampions.beastDfs_m * playerChampions.lastStand_m;
        projectile = championData.projectile;
        cooldownTime = championData.cooldownTime * playerChampions.squirl_m[2] * (2 - playerChampions.lastStand_m);
        
        if (isEnchanted)
        {
            cooldownTime *= playerChampions.enchanted_m;
        }

        if (championData.damagingType == DamagingType.Aoe)
        {
            damage *= playerChampions.amplify_m;
        }
        if (championData.damagingType == DamagingType.Range)
        {
            damage *= playerChampions.ballista_m;
        }

        foreach (Class @class in championData.classes)
        {
            switch (@class)
            {
                case Class.Bird:
                    cooldownTime *= playerChampions.birdCooldown_m;
                    break;
                case Class.Bug:
                    damage *= playerChampions.bugDmg_m;
                    break;
                case Class.Aquatic:
                    if (playerChampions.playerItems.Contains("Chronomancy"))
                    {
                        cooldownTime *= 0.85f;
                    }
                    break;
            }
        }
        cooldownTime_o = cooldownTime;
    }

    public void SetCharacteristicsForEnemy()
    {
        maxHealth = championData.health + 15 * (GameManager.Instance.currentLevel - 1) * playerChampions.intimidation_m;
        health = maxHealth;
        damage = championData.damage + 4 * (GameManager.Instance.currentLevel - 1);
        defense = championData.defense + 1.6f * (GameManager.Instance.currentLevel - 1);
        projectile = championData.projectile;
        cooldownTime = championData.cooldownTime;

        if (playerChampions.reptileIgnoreShield)
        {
            defense = 0;
        }
    }

    public void SetCharacteristicsForBoss()
    {
        maxHealth = championData.health + 15 * (GameManager.Instance.currentLevel - 1) * playerChampions.intimidation_m * 3;
        health = maxHealth;
        damage = championData.damage + 4 * (GameManager.Instance.currentLevel - 1) * 2;
        defense = championData.defense + 1.6f * (GameManager.Instance.currentLevel - 1) * 3;
        projectile = championData.projectile;
        cooldownTime = championData.cooldownTime * 0.9f;

        if (playerChampions.reptileIgnoreShield)
        {
            defense = 0;
        }
    }


    public virtual void TakeDamage(float damage, LivingEntity damagingEntity)
    {
        if (Time.time >= nextImmortalTime)
        {
            nextImmortalTime = Time.time + immortalTime;

            float incomeDamage = damage * (100 / (100 + defense));

            if (CompareTag("Enemy"))
            {
                if (playerChampions.playerItems.Contains("Stunning") && Utility.RandomBool(10f))
                {
                    GetComponent<Enemy>().stunTime = 2f;
                }
                if (playerChampions.playerItems.Contains("Critical") && Utility.RandomBool(15f))
                {
                    incomeDamage *= 2f;
                }
                health -= incomeDamage * playerChampions.vulnerability_m;
            }
            else
            {
                health -= incomeDamage;
                if (playerChampions.beastar_m && championData.classes.Contains(Class.Beast))
                {
                    cooldownTime = cooldownTime_o * (1 - (1 - health / maxHealth) * 0.5f);
                }
                unitsUI.LoadHealthbar(this.gameObject, health, maxHealth);
            }

            // Damage Popup
            if (incomeDamage > 0)
            {
                TMP_Text i_damagePopup = Instantiate(damagePopup, transform.position, Quaternion.identity, damagePopupHolder.transform);
                i_damagePopup.text = Mathf.RoundToInt(incomeDamage).ToString();
                if (damagingEntity != null && damagingEntity.CompareTag("Champion"))
                {
                    i_damagePopup.color = damagingEntity.championData.cardColor.nameBoxColor;
                }
            }

            skeletonAnimation.state.SetAnimation(0, "defense/hit-by-ranged-attack", false);
            skeletonAnimation.state.AddAnimation(0, "draft/run-origin", true, 0);
        }

        if (gameObject.CompareTag("Enemy") && playerChampions.playerItems.Contains("Culling") && health / maxHealth <= 0.2f)
        {
            Die();
        }
        else if (health <= 0)
        {
            Die();
        }
    }

    public void Healing(float healAmount)
    {
        GameObject vfx_heal = Resources.Load("Prefabs/vfx_heal") as GameObject;
        Instantiate(vfx_heal, transform.position, Quaternion.identity);
        vfx_heal.GetComponent<HealEffect>().target = this.gameObject;

        health += healAmount * playerChampions.blessing_m;

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

        if (gameObject.CompareTag("Champion"))
        {
            if (playerChampions.playerItems.Contains("Last Stand"))
            {
                int count = 0;
                Champion champion = new Champion();
                foreach (GameObject championGO in playerChampions.champions)
                {
                    if (championGO.activeSelf && championGO != this.gameObject)
                    {
                        champion = championGO.GetComponent<Champion>();
                        Debug.Log(champion.defense);
                        count++;
                    }
                }
                if (count == 1)
                {
                    playerChampions.lastStand_m = 1.2f;
                    champion.SetCharacteristics();
                }
            }
            foreach (GameObject championGO in playerChampions.champions)
            {
                if (championGO.activeSelf && championGO != this.gameObject)
                {
                    championGO.GetComponent<Champion>().defense *= playerChampions.hardening_m;
                }
            }
        }

        SoundManager.Instance.PlayDeathSound();

        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        transform.DOKill();
    }

    private void BuildAxie()
    {
        var adultCombo = new Dictionary<string, string> {
            {"back", championData.bodies[0].@class.ToString().ToLower() + "-0" + championData.bodies[0].value.ToString()},
            {"body", "body-" + championData.specialBody.ToString().ToLower()},
            {"ears", championData.bodies[1].@class.ToString().ToLower() + "-0" + championData.bodies[1].value.ToString()},
            {"ear", championData.bodies[2].@class.ToString().ToLower() + "-0" + championData.bodies[2].value.ToString()},
            {"eyes", championData.bodies[3].@class.ToString().ToLower() + "-0" + championData.bodies[3].value.ToString()},
            {"horn", championData.bodies[4].@class.ToString().ToLower() + "-0" + championData.bodies[4].value.ToString()},
            {"mouth", championData.bodies[5].@class.ToString().ToLower() + "-0" + championData.bodies[5].value.ToString()},
            {"tail", championData.bodies[6].@class.ToString().ToLower() + "-0" + championData.bodies[6].value.ToString()},
            {"body-class", championData.bodies[7].@class.ToString().ToLower()},
            {"body-id", " 2727 "},
        };

        adultCombo["back"] = adultCombo["back"].Replace("-010", "-10").Replace("-012", "-12");
        adultCombo["ears"] = adultCombo["ears"].Replace("-010", "-10").Replace("-012", "-12");
        adultCombo["ear"] = adultCombo["ear"].Replace("-010", "-10").Replace("-012", "-12");
        adultCombo["eyes"] = adultCombo["eyes"].Replace("-010", "-10").Replace("-012", "-12").Replace("-06", "-02").Replace("-12", "-04"); ;
        adultCombo["horn"] = adultCombo["horn"].Replace("-010", "-10").Replace("-012", "-12");
        adultCombo["mouth"] = adultCombo["mouth"].Replace("-010", "-10").Replace("-012", "-12").Replace("-06", "-02").Replace("-12", "-04"); ;
        adultCombo["tail"] = adultCombo["tail"].Replace("-010", "-10").Replace("-012", "-12");

        Debug.Log(adultCombo["back"] + " " + adultCombo["body"] + " " + adultCombo["ears"]
                    + " " + adultCombo["ear"] + " " + adultCombo["eyes"] + " " + adultCombo["horn"]
                    + " " + adultCombo["mouth"] + " " + adultCombo["tail"] + " " + adultCombo["body-class"]);

        float scale = 0.0016f;
        byte colorVariant = (byte)Mixer.Builder.GetSampleColorVariant(championData.characterClass, championData.classValue);
        var result = Mixer.Builder.BuildSpineAdultCombo(adultCombo, colorVariant, scale);
        skeletonAnimation.skeletonDataAsset = result.skeletonDataAsset;
        skeletonAnimation.Initialize(true);
        if (result.adultCombo.ContainsKey("body") &&
            result.adultCombo["body"].Contains("mystic") &&
            result.adultCombo.TryGetValue("body-class", out var bodyClass) &&
            result.adultCombo.TryGetValue("body-id", out var bodyId))
        {
            skeletonAnimation.gameObject.AddComponent<MysticIdController>().Init(bodyClass, bodyId);
        }
    }
}
