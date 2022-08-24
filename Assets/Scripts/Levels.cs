using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Level
{
    public static float[][] levelToTierChances =
    {
        new float[] {100, 0, 0},
        new float[] {95, 5, 0},
        new float[] {90, 10, 0},
        new float[] {85, 10, 5},
        new float[] {80, 15, 5},
        new float[] {75, 20, 5},
        new float[] {70, 20, 10},
        new float[] {65, 25, 10},
        new float[] {60, 30, 10},
        new float[] {55, 30, 15},
        new float[] {50, 35, 15},
        new float[] {45, 35, 20},
        new float[] {40, 40, 20},
        new float[] {35, 40, 25},
        new float[] {30, 40, 30},
        new float[] {25, 45, 30},
        new float[] {20, 45, 35},
        new float[] {20, 40, 40},
        new float[] {15, 40, 45},
        new float[] {15, 35, 50},
        new float[] {10, 30, 60},
        new float[] {10, 20, 70},
        new float[] {5, 15, 80},
        new float[] {0, 10, 90},
        new float[] {0, 0, 100}
    };


    public static float[][] levelToEliteSpawnWeight = new float[][]
    {
        new float[] {0},
        new float[] {4, 2},
        new float[] {10},
        new float[] {4, 4},
        new float[] {4, 3, 2},
        new float[] {12},
        new float[] {5, 3, 2},
        new float[] {6, 3, 3, 3},
        new float[] {14},
        new float[] {8, 4},
        new float[] {8, 6, 2},
        new float[] {16},
        new float[] {8, 8},
        new float[] {12, 6},
        new float[] {18},
        new float[] {10, 6, 4},
        new float[] {6, 5, 4, 3},
        new float[] {18},
        new float[] {10, 6},
        new float[] {8, 6, 2},
        new float[] {22},
        new float[] {10, 8, 4},
        new float[] {20, 5, 5},
        new float[] {30},
        new float[] {5, 5, 5, 5, 5, 5},
    };
    
    public static string[][] levelToEliteSpawnType =
    {
        new string[] {"speed"},
        new string[] {"speed", "shooter"},
        new string[] {"speed"},
        new string[] {"speed", "exploder"},
        new string[] {"speed", "exploder", "shooter"},
        new string[] {"speed"},
        new string[] {"speed", "exploder", "header"},
        new string[] {"speed", "exploder", "header", "shooter"},
        new string[] {"shooter"},
        new string[] {"exploder", "header"},
        new string[] {"exploder", "header", "speed"},
        new string[] {"exploder"},
        new string[] {"speed", "shooter"},
        new string[] {"speed", "exploder"},
        new string[] {"shooter"},
        new string[] {"speed", "exploder", "shooter"},
        new string[] {"speed", "exploder", "header", "shooter"},
        new string[] {"shooter"},
        new string[] { "speed", "header"},
        new string[] {"speed", "exploder", "shooter"},
        new string[] {"shooter"},
        new string[] {"speed", "header", "shooter"},
        new string[] { "speed", "exploder", "shooter"},
        new string[] {"speed"},
        new string[] {"speed", "exploder", "shooter", "header", "shooter", "exploder"},
    };


    public static int[] levelToMaxWaves =
    {
        2, 3, 3, 4, 
        3, 4, 4, 5,
        4, 5, 5, 6,
        5, 6, 6, 7,
        6, 7, 7, 8,
        8, 9, 9, 10, 12
    };



    public static Vector2[] levelToMoneyRange =
    {
        new Vector2 (3, 3),
        new Vector2 (3, 4),
        new Vector2 (4, 5),
        new Vector2 (5, 6),
        new Vector2 (6, 8),
        new Vector2 (8, 10),
        new Vector2 (10, 12), 
        new Vector2 (12, 13),
        new Vector2 (13, 14),
        new Vector2 (14, 15),
        new Vector2 (15, 16),
        new Vector2 (16, 17),
        new Vector2 (15, 16),
        new Vector2 (14, 15),
        new Vector2 (14, 16),
        new Vector2 (16, 18),
        new Vector2 (18, 20),
        new Vector2 (20, 20), 
        new Vector2 (20, 22),
        new Vector2 (22, 25), 
        new Vector2 (20, 25),
        new Vector2 (25, 28),
        new Vector2 (28, 30),
        new Vector2 (30, 32),
        new Vector2 (32, 32)
    };

    public static List<Item> Items = new List<Item>
    {
        new Item("Amplify", "+20% AoE damage"),
        new Item("Resonance" , "All AoE attacks deal +3% damage per unit hit"),
        new Item("Ballista", "+20% projectile damage"),
        new Item("Crucio", "Taking damage also shares that across all enemies at 20% its value"),
        new Item("Kinetic Bomb", "When an axie dies it explodes, pushing enemies away"),
        new Item("Porcupine", "When an axie dies it explodes, releasing piercing and ricocheting projectiles"),
        new Item("Last Stand", "The last unit alive is fully healed and receives a +20% bonus to all stats"),
        new Item("Seeping", "Enemies taking DoT damage have -15% defense"),
        new Item("Deceleration", "Enemies taking DoT damage have -15% movement speed"),
        new Item("Annihilation", "When a bird dies deal its DoT damage to all enemies for 3 seconds"),
        new Item("Blunt", "Projectile have +10% chance to knockback"),
        new Item("Chronomancy", "Aquatic cast their spells 15% faster"),
        new Item("Barrage", "30% chance to create secondary AoE on AoE hit"),
        new Item("Enchanted", "+33% attack speed to a random axie"),
        new Item("Beastar", "All beasts have up to +50% attack speed based on missing HP"),
        new Item("Blessing", "+20% healing effectiveness"),
        new Item("Intimidation", "Enemies spawn with -20% max HP"),
        new Item("Vulnerability", "Enemies take +10 damage"),
        new Item("Temporal", "Enemies are 10 slower"),
        new Item("Ceremonial", "Killing an enemy fires a dagger"),
        new Item("Critical", "15% chance for attacks to critically strike, dealing 2x damage"),
        new Item("Kinetic Strike", "20% chance for attacks to push enemies away with high force"),
        new Item("Stunning", "16% chance for attacks to stun for 2 seconds"),
        new Item("Culling", "Instantly kill enemy if below 20% max HP"),
        new Item("Hardening", "+50% defense to all axies for every an axie dies")
    };

    public class Item
    {
        public string name;
        public string description;
        public string color;

        public Item(string _name, string _description)
        {
            name = _name;
            description = _description;
        }

        public Item(string _name, string _description, string _color)
        {
            name = _name;
            description = _description;
            color = _color;
        }
    };
}
