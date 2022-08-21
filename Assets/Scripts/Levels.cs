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
        new string[] {"speed", "exploder", "head"},
        new string[] {"speed", "exploder", "head", "shooter"},
        new string[] {"shooter"},
        new string[] {"exploder", "head"},
        new string[] {"exploder", "head", "speed"},
        new string[] {"exploder"},
        new string[] {"speed", "shooter"},
        new string[] {"speed", "exploder"},
        new string[] {"shooter"},
        new string[] {"speed", "exploder", "shooter"},
        new string[] {"speed", "exploder", "head", "shooter"},
        new string[] {"shooter"},
        new string[] { "speed", "head"},
        new string[] {"speed", "exploder", "shooter"},
        new string[] {"shooter"},
        new string[] {"speed", "head", "shooter"},
        new string[] { "speed", "exploder", "shooter"},
        new string[] {"speed"},
        new string[] {"speed", "exploder", "shooter", "head", "shooter", "exploder"},
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

    
}
