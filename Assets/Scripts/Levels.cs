using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Level
{
    public static float[][] levelToTierChances =
    {
        new float[] {90, 10, 0, 0},
        new float[] {80, 15, 5, 0},
        new float[] {75, 20, 5, 0},
        new float[] {70, 20, 10, 0},
        new float[] {70, 20, 10, 0},
        new float[] {65, 25, 10, 0},
        new float[] {60, 25, 15, 0},
        new float[] {55, 25, 15, 5},
        new float[] {50, 30, 15, 5},
        new float[] {50, 30, 15, 5},
        new float[] {45, 30, 20, 5},
        new float[] {40, 30, 20, 10},
        new float[] {35, 30, 25, 10},
        new float[] {30, 30, 25, 15},
        new float[] {25, 30, 30, 15},
        new float[] {25, 25, 30, 20},
        new float[] {20, 25, 35, 20},
        new float[] {15, 25, 35, 25},
        new float[] {10, 25, 40, 25},
        new float[] {5, 25, 40, 30},
        new float[] {0, 25, 40, 35},
        new float[] {0, 20, 40, 40},
        new float[] {0, 20, 35, 45},
        new float[] {0, 10, 30, 60},
        new float[] {0, 0, 0, 100}
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
    /*
    public static string[][] levelToEliteSpawnType =
    {
        new string[] {"speed"},
        new string[] {"speed", "shooter"},
        new string[] {"speed"},
        new string[] {"speed", "exploder"},
        new string[] {"speed", "exploder", "shooter"},
        new string[] {"speed"},
        new string[] {"speed", "exploder", "headbutter"},
        new string[] {"speed", "exploder", "headbutter", "shooter"},
        new string[] {"shooter"},
        new string[] {"exploder", "headbutter"},
        new string[] {"exploder", "headbutter", "tank"},
        new string[] {"exploder"},
        new string[] {"speed", "shooter"},
        new string[] {"speed", "spawner"},
        new string[] {"shooter"},
        new string[] {"speed", "exploder", "spawner"},
        new string[] {"speed", "exploder", "spawner", "shooter"},
        new string[] {"spawner"},
        new string[] {"headbutter", "tank"},
        new string[] {"speed", "tank", "spawner"},
        new string[] {"headbutter"},
        new string[] {"speed", "headbutter", "tank"},
        new string[] {"headbutter", "tank", "shooter"},
        new string[] {"tank"},
        new string[] {"speed", "exploder", "headbutter", "tank", "shooter", "spawner"},
    };*/

    public static string[][] levelToEliteSpawnType =
    {
        new string[] {"speed"},
        new string[] {"speed", "shooter"},
        new string[] {"shooter"},
        new string[] {"shooter", "shooter"},
        new string[] {"shooter", "shooter", "shooter"},
        new string[] {"shooter"},
        new string[] {"shooter", "shooter", "shooter"},
        new string[] {"speed", "shooter", "shooter", "speed"},
        new string[] {"speed"},
        new string[] {"speed", "shooter"},
        new string[] {"speed", "shooter", "speed"},
        new string[] {"speed"},
        new string[] {"speed", "shooter"},
        new string[] {"speed", "shooter"},
        new string[] {"speed"},
        new string[] {"speed", "shooter", "speed"},
        new string[] {"speed", "speed", "speed", "speed"},
        new string[] {"spawner"},
        new string[] { "shooter", "shooter"},
        new string[] {"speed", "shooter", "shooter"},
        new string[] {"shooter"},
        new string[] {"speed", "shooter", "shooter"},
        new string[] {"shooter", "shooter", "shooter"},
        new string[] {"shooter"},
        new string[] {"speed", "shooter", "shooter", "shooter", "shooter", "shooter"},
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
        new Vector2 (3, 3),
        new Vector2 (5, 6),
        new Vector2 (4, 5),
        new Vector2 (5, 8),
        new Vector2 (8, 10),
        new Vector2 (8, 10), 
        new Vector2 (12, 14),
        new Vector2 (14, 18),
        new Vector2 (10, 13),
        new Vector2 (12, 15),
        new Vector2 (18, 20),
        new Vector2 (10, 14),
        new Vector2 (12, 16),
        new Vector2 (14, 18),
        new Vector2 (12, 12),
        new Vector2 (12, 15),
        new Vector2 (20, 28), 
        new Vector2 (10, 16),
        new Vector2 (10, 18), 
        new Vector2 (20, 28),
        new Vector2 (32, 32),
        new Vector2 (36, 36),
        new Vector2 (48, 48),
        new Vector2 (100, 100)
    };

    
}
