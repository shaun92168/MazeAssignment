using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class Ball : MonoBehaviour
{

    private Vector3 direction;
    public float speed;

    [SerializeField] private int playerOneScore = 0;
    [SerializeField] private int playerTwoScore = 0;

    public Vector3 spawnPoint;

    public Text playerOneText; 
    public Text playerTwoText;
    public Text winnerText; 

    public GameObject goalEffect;
    public GameObject ballTrail; 
    private bool gameOver = false;
    public Button playAgain;
    public Button returnToMenu; 

    void Start()
    {
        playerOneScore = 0;
        playerTwoScore = 0;
        spawnPoint = this.transform.position; 
        this.direction = new Vector3(1f, 0f, 1f);
        winnerText.enabled = false;
        playAgain.gameObject.SetActive(false);
        returnToMenu.gameObject.SetActive(false);
    }

    void Update()
    {
        this.transform.position += direction * speed * Time.deltaTime;
        playerTwoText.text = playerTwoScore.ToString();
        playerOneText.text = playerOneScore.ToString();

        if (gameOver == true)
        {
            speed = 0;
            Destroy(ballTrail); 
            this.transform.position = new Vector3(0, 0, 0);
            winnerText.enabled = true;
            playAgain.gameObject.SetActive(true);
            returnToMenu.gameObject.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
        }
    }

    void OnCollisionEnter(Collision col)
    {
        Vector3 normal = col.contacts[0].normal;
        direction = Vector3.Reflect(direction, normal);

        if (col.gameObject.name == "WestWall")
        {
            playerTwoScore++;
            Instantiate(goalEffect, this.transform.position, Quaternion.identity); 
            transform.position = spawnPoint; 
            if (playerTwoScore >= 5)
            {
                winnerText.text = "Player 2 Wins"; 
                gameOver = true; 
            }
        }

        if (col.gameObject.name == "EastWall")
        {
            playerOneScore++;
            Instantiate(goalEffect, this.transform.position, Quaternion.identity);
            transform.position = spawnPoint;
            if (playerOneScore >= 5)
            {
                winnerText.text = "Player 1 Wins";
                gameOver = true;
            }
        }
    }
}
