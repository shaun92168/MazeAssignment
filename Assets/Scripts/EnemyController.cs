﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyController : MonoBehaviour
{
    PlayerController Player;
    public float speed = 0.1f;
    private float rotAmount;
    // private Rigidbody rb;
    private Vector3 movement;
    private Vector3 rvec;
    public int lives;
    public Vector3[] spawnPositions = new[] { new Vector3(-3.65f, 0.003f, -1.38f), new Vector3(-5.6f, 0.003f, -3.57f), new Vector3(-2.54f, 0.003f, -6.53f), new Vector3(-9.57f, 0.003f, -9.51f),
    new Vector3(-12.76f, 0.003f, -3.48f), new Vector3(-22.64f, 0.003f, -9.58f), new Vector3(-17.65f, 0.003f, -12.55f) };
    private System.Random rand = new System.Random();
    // Start is called before the first frame update
    void Start()
    {
        // rb = GetComponent<Rigidbody>();
        Player = FindObjectOfType<PlayerController>();
        rvec = new Vector3(0,1,0);
        movement = new Vector3(0, 0, -1);
    }

    // Update is called once per frame
    void Update()
    {
        if (lives <= 0)
        {
            RespawnEnemy();
        }
    }

    void FixedUpdate()
    {
        moveCharacter(movement);
    }

    void moveCharacter(Vector3 direction)
    {
        // rb.MovePosition(transform.position + (direction * speed * Time.deltaTime));
        transform.Translate(direction * speed * Time.deltaTime);

    }

    void OnCollisionEnter(Collision col)
    {
        if(col.collider.name == "Quad4" || col.collider.name == "Quad1" || col.collider.name == "Quad2" || col.collider.name == "Quad3")
        {
            rotAmount = Random.Range(90, 270);
            transform.Rotate(rvec, rotAmount);
        }

        if(col.collider.name == "Player")
        {
            SceneManager.LoadScene("SampleScene");
            Debug.Log("player");
        }
    } 

    void OnCollisionStay(Collision col)
    {
        if(col.collider.name == "Quad4" || col.collider.name == "Quad1" || col.collider.name == "Quad2" || col.collider.name == "Quad3")
        {
            rotAmount = Random.Range(90, 270);
            transform.Rotate(rvec, rotAmount);
        }
    } 

    private Vector3 RandomVector(float min, float max) {
        var x = Random.Range(min, max);
        var y = Random.Range(min, max);
        var z = Random.Range(min, max);
        return new Vector3(x, y, z);
    }

    public IEnumerator WaitCoroutine()
    {
        yield return new WaitForSeconds(5.0f);
    }

    private void RespawnEnemy()
    {
        movement = Vector3.zero;
        int randomSpawn = rand.Next(0, spawnPositions.Length);
        this.gameObject.transform.position = spawnPositions[randomSpawn];
        StartCoroutine(WaitCoroutine());
        movement = new Vector3(0, 0, -1);
        lives = 3;
    }

    public int getLives()
    {
        return this.lives;
    }
}
