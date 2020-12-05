using System.Collections;
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

    //audio
    public GameObject die;
    public GameObject hitsnd;
    public AudioSource deathSound;
    public AudioSource hitSound;

    // Start is called before the first frame update
    void Start()
    {
        // rb = GetComponent<Rigidbody>();
        Player = FindObjectOfType<PlayerController>();
        rvec = new Vector3(0, 1, 0);
        movement = new Vector3(0, 0, -1);

        //audio
        die = GameObject.Find("die");
        deathSound = die.GetComponent<AudioSource>();
        hitsnd = GameObject.Find("hitsnd");
        hitSound = hitsnd.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (lives <= 0)
        {
            Player.RespawnEnemy();
            deathSound.Play();
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
        if (col.collider.name == "Quad4" || col.collider.name == "Quad1" || col.collider.name == "Quad2" || col.collider.name == "Quad3" 
            || col.collider.name == "DoorL" || col.collider.name == "DoorR" || col.collider.name == "DoorB")
        {
            rotAmount = Random.Range(90, 270);
            transform.Rotate(rvec, rotAmount);
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

    public int getLives()
    {
        return this.lives;
    }
}
