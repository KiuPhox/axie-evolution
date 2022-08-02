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
        new float[] {0, 0, 0, 100},
    };

    public static int[] levelToMaxWaves =
    {
        2, 3, 3, 4, 
        3, 4, 4, 5,
        4, 5, 5, 6,
        5, 6, 6, 7,
        7, 8, 8, 9
    };
}
