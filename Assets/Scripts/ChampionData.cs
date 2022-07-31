using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Champion Data", menuName = "Champion Data")]
public class ChampionData : ScriptableObject
{
    public new string name;
    public string description;
    public float health;
    public float damage;
    public float defense;
    public float range;
    public float cooldownTime;
    public int tier;
    public CardColorData cardColor;
    public GameObject projectile;
    [HideInInspector] public float weight;
}
