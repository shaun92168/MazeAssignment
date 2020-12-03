using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;

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
    public Canvas escMenu;
    private bool paused = false;
    private IEnumerator coroutine;
    private GameObject PongEntrance;

    public static PlayerController pCtrl;
    private Vector3 playerPos;
    private Vector3 enemyPos;
    private int playerScore;
    const string fileName = "/mazeSave.dat";

    private void Awake()
    {
        if (pCtrl == null)
        {
            DontDestroyOnLoad(gameObject);
            pCtrl = this;
            pCtrl.score = 0;
            LoadState();
        }
    }
    void Start()
    {
        GameObject enemy = Instantiate(Enemy, Enemy.transform.position, Enemy.transform.rotation);
        toggleText.enabled = false;
        scoreText.enabled = true;
        Walls = GameObject.FindGameObjectsWithTag("Walls");
        PongEntrance = GameObject.FindGameObjectWithTag("Pong");
        WinScreen.SetActive(false);
    }

    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Home))
        {
            Cursor.lockState = CursorLockMode.Locked;
            SceneManager.LoadScene("SampleScene");
            Time.timeScale = 1;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            toggleEscMenu();
        }

        void toggleEscMenu()
        {
            if (paused)
            {
                Time.timeScale = 1;
                escMenu.gameObject.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
            } else {
                Time.timeScale = 0;
                escMenu.gameObject.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
            }
            paused = !paused;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            ToggleWalls();
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) && Time.timeScale != 0)
        {
            GameObject thrownBall = Instantiate(Ball, BallSpawn.position, BallSpawn.rotation);
            thrownBall.GetComponent<Rigidbody>().velocity = thrownBall.transform.forward * ballSpeed;
        }

        scoreText.text = "Score: " + pCtrl.score;

        if (this.gameObject.transform.position.y != 0.27)
        {
            this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x, 0.27f, this.gameObject.transform.position.z);
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.name == "END")
        {
            WinScreen.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
        }

        if (col.name == "DoorB")
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
        }
        else
        {
            toggleText.text = "Wall Collision Disabled";
        }

        coroutine = ShowMessage(toggleText.text, 1.0f);
        StartCoroutine(coroutine);

        IEnumerator ShowMessage(string message, float delay)
        {
            toggleText.enabled = true;
            yield return new WaitForSeconds(delay);
            toggleText.enabled = false;
        }
    }

    public void LoadState()
    {
        if (File.Exists(Application.persistentDataPath + fileName))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = File.Open(Application.persistentDataPath + fileName, FileMode.Open, FileAccess.Read);
            GameData data = (GameData)bf.Deserialize(fs);
            fs.Close();
            pCtrl.score = data.score;
        }
    }

    public void SaveState()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = File.Open(Application.persistentDataPath + fileName, FileMode.OpenOrCreate);
        GameData data = new GameData();
        data.score = pCtrl.score;
        bf.Serialize(fs, data);
        fs.Close();
    }
}

[Serializable]
class GameData
{
    public int score;
}