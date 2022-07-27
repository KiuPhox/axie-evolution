using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChampSeparation : MonoBehaviour
{
    GameObject[] others;
    public float desiredSeparation = 1.5f;
    public float separationStrength;
    Vector3 diff;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        others = GameObject.FindGameObjectsWithTag("Player");
        int count = 0;
        foreach(GameObject other in others)
        {
            if (other != gameObject)
            {
                float d = Vector3.Distance(other.transform.position, this.transform.position);
                if (d > 0 && d < desiredSeparation)
                {
                    diff = transform.position - other.transform.position;
                    diff.Normalize();
                    count++;
                }
            }
        }

        if (count > 0)
        {
            diff /= count;
            transform.Translate(diff * Time.deltaTime * separationStrength);
        }
    }
}
