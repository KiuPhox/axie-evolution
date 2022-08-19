using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "New Champion Data", menuName = "Champion Data")]
public class ChampionData : ScriptableObject
{
    [Header("Basic Info")]
    public new string name;
    [TextArea(5, 5)]
    public string description;

    [HorizontalGroup("Game Data", 120, MarginRight = 20)]
    [PreviewField(120)]
    [HideLabel]
    public Sprite sprite;

    [VerticalGroup("Game Data/Stats"), LabelWidth(100)]
    public float health;
    [VerticalGroup("Game Data/Stats"), LabelWidth(100)]
    public float damage;
    [VerticalGroup("Game Data/Stats"), LabelWidth(100)]
    public float defense;
    [VerticalGroup("Game Data/Stats"), LabelWidth(100)]
    public float range;
    [VerticalGroup("Game Data/Stats"), LabelWidth(100)]
    public float cooldownTime;
    [VerticalGroup("Game Data/Stats"), LabelWidth(100)]
    public int tier;

    [Header("Others")]
    public string axieID;
    public string genes;
    public GameObject projectile;
    public CardColorData cardColor;
    [HideInInspector] public float weight;
    public string attackAnimation;

    public List<Class> classes = new List<Class>();
}

public enum Class{
   Beast,
   Aquatic,
   Plant,
   Bug,
   Bird,
   Reptile,
   Mech,
   Dawn,
   Dusk
};
