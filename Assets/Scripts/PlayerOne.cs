using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOne : MonoBehaviour
{
    public float speed;
    public Rigidbody rb; 

    void Start()
    {
        rb = GetComponent<Rigidbody>(); 
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            rb.velocity = new Vector3(0, 0, speed); 
        } else if (Input.GetKey(KeyCode.S))
        {
            rb.velocity = new Vector3(0, 0, -speed);
        } else
        {
            rb.velocity = new Vector3(0, 0, 0);
        }
    }
}
