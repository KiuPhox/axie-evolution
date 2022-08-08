using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Borders : MonoBehaviour
{
    public GameObject[] borders;
    public Vector2 offset;

    Vector2 worldBoundary;
    void Start()
    {
        worldBoundary = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        borders[0].transform.position = new Vector2(0, worldBoundary.y + offset.y);
        borders[1].transform.position = new Vector2(0, -worldBoundary.y - offset.y);
        borders[2].transform.position = new Vector2(-worldBoundary.x - offset.x, 0);
        borders[3].transform.position = new Vector2(worldBoundary.x + offset.x, 0);
    }
}
