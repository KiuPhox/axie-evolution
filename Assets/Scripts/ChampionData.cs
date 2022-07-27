using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Champion Data", menuName = "Champion Data")]
public class ChampionData : ScriptableObject
{
    public new string name;
    public float health;
    public int damage;
    public int range;
    public float cooldownTime;
    public int tier;
    [HideInInspector] public float weight;
}
