using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Autodestroy : MonoBehaviour
{
    public float TimeToDestroy;

    float StartTime;

    void Start()
    {
        StartTime = Time.time;
    }

    void Update()
    {
        if(Time.time > StartTime + TimeToDestroy)
            Destroy(gameObject);
    }

}
