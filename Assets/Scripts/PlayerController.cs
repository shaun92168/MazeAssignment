using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public CharacterController controller;
    public GameObject Enemy;
    public GameObject WinScreen;
    public GameObject Ball;
    public Transform BallSpawn;
    public float speed = 12f;
    public float ballSpeed = 200;
    public GameObject[] Walls;
    public Text toggleText;
    private bool toggleStatus = true;
    public Text scoreText;
    public int score;
    private IEnumerator coroutine;
    private GameObject PongEntrance;

    void Start()
    {
        GameObject enemy = Instantiate(Enemy, Enemy.transform.position, Enemy.transform.rotation);
        toggleText.enabled = false;
        scoreText.enabled = true;
        Walls = GameObject.FindGameObjectsWithTag("Walls");
        PongEntrance = GameObject.FindGameObjectWithTag("Pong");
        WinScreen.SetActive(false);
        score = 0;
    }

    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        if(Input.GetKeyDown(KeyCode.Home))
        {
            Cursor.lockState = CursorLockMode.Locked; 
            SceneManager.LoadScene("SampleScene");
            Time.timeScale = 1;
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            ToggleWalls();
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            GameObject thrownBall = Instantiate(Ball, BallSpawn.position, BallSpawn.rotation);
            thrownBall.GetComponent<Rigidbody>().velocity = thrownBall.transform.forward * ballSpeed;
        }

        scoreText.text = "Score: " + score;

        if (this.gameObject.transform.position.y != 0.27)
        {
            this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x, 0.27f, this.gameObject.transform.position.z);
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.name == "END")
        {
            WinScreen.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
        }

        if(col.name == "DoorB")
        {
            Debug.Log("Enter Pong");
            SceneManager.LoadScene("PongAI", LoadSceneMode.Single);
        }
        
        Debug.Log(col.name);
    }

    void ToggleWalls()
    {
        foreach (GameObject wall in Walls)
        {
            Physics.IgnoreCollision(wall.GetComponent<Collider>(), GetComponent<Collider>(), toggleStatus);
        }

        toggleStatus = !toggleStatus;

        if (toggleStatus)
        {
            toggleText.text = "Wall Collision Enabled";
        } else
        {
            toggleText.text = "Wall Collision Disabled";
        }

        coroutine = ShowMessage(toggleText.text, 1.0f);
        StartCoroutine(coroutine);

        IEnumerator ShowMessage (string message, float delay)
        {
            toggleText.enabled = true;
            yield return new WaitForSeconds(delay);
            toggleText.enabled = false;
        }
    }
}
