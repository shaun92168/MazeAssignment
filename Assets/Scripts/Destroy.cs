using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    public float destroyTime = 2;
    private float temp; 
    void Start()
    {
        temp = destroyTime; 
    }

    void Update()
    {
        temp -= Time.deltaTime; 
        if (temp <= 0)
        {
            Destroy(gameObject); 
        }
    }
}
