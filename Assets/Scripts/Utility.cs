using System.Collections;
using UnityEngine;

public static class Utility
{

    public static T[] ShuffleArray<T>(T[] array, int seed)
    {
        System.Random prng = new System.Random(seed);

        for (int i = 0; i < array.Length - 1; i++)
        {
            int randomIndex = prng.Next(i, array.Length);
            T tempItem = array[randomIndex];
            array[randomIndex] = array[i];
            array[i] = tempItem;
        }
        return array;
    }

    public static T[] ShuffleArray<T>(T[] array)
    {
        for (int i = 0; i < array.Length - 1; i++)
        {
            int randomIndex = Random.Range(0, array.Length);
            T tempItem = array[randomIndex];
            array[randomIndex] = array[i];
            array[i] = tempItem;
        }
        return array;
    }

    static Vector3 worldBoundary = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

    public static Vector3 GetMouseWorldPosition()
    {
        Vector3 screenPosition = Input.mousePosition;
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
        return new Vector3(worldPosition.x, worldPosition.y, 0f);
    }

    public static int WeightPick(float[] weights)
    {
        float accumlateWeights = 0;
        float[] anotherWeights = weights;
        for (int i = 0; i < weights.Length; i++)
        {
            accumlateWeights += weights[i];
            anotherWeights[i] = accumlateWeights;
        }
        float r = Random.Range(0f, 1f) * accumlateWeights;
        for (int i = 0; i < anotherWeights.Length; i++)
        {
            if (anotherWeights[i] >= r)
            {
                return i;
            }
        }
        return -1;
    }

}