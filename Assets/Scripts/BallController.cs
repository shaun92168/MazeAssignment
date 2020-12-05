using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    PlayerController Player;
    private float expiryTime = 2f;
    public GameObject ballsnd;
    public AudioSource ballSound;
    public GameObject hitsnd;
    public AudioSource hitSound;

    private void Start()
    {
        Destroy(gameObject, expiryTime);
        Player = FindObjectOfType<PlayerController>();
        ballsnd = GameObject.Find("ballsnd");
        ballSound = ballsnd.GetComponent<AudioSource>();
        hitsnd = GameObject.Find("hitsnd");
        hitSound = hitsnd.GetComponent<AudioSource>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Physics.IgnoreCollision(this.GetComponent<Collider>(), collision.gameObject.GetComponent<Collider>());
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<EnemyController>().lives -= 1;
            Player.score++;
            Destroy(gameObject);
        }
        if (collision.collider.name == "Quad4" || collision.collider.name == "Quad1" || collision.collider.name == "Quad2" || collision.collider.name == "Quad3"
        || collision.collider.name == "DoorL" || collision.collider.name == "DoorR" || collision.collider.name == "DoorB")
        {
            ballSound.Play();
        }
        if (collision.collider.tag == "Floor")
        {
            ballSound.Play();
        }
        if (collision.collider.tag == "Enemy")
        {
            hitSound.Play();
        }
    }
}
