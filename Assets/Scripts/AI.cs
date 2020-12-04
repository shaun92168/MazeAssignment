using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    public float speed = 8.0f;
    public Rigidbody rb;

    private bool dirUP = true; 

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        if (dirUP)
        {
            rb.velocity = new Vector3(0, 0, speed);
        } else
        {
            rb.velocity = new Vector3(0, 0, -speed);
        }

        if(rb.position.z >= 9)
        {
            dirUP = false;
        } 

        if (rb.position.z <= -9)
        {
            dirUP = true; 
        }
    }
}