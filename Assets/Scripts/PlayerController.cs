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
    private GameObject enemy;
    public GameObject WinScreen;
    public GameObject Ball;
    public Transform BallSpawn;
    public float speed = 12f;
    public float ballSpeed = 200;
    public GameObject[] Walls;
    public Text toggleText;
    private bool toggleStatus = true;
    public Text scoreText;
    public Canvas escMenu;
    private bool paused = false;
    private IEnumerator coroutine;
    private GameObject PongEntrance;

    public static PlayerController pCtrl;
    private Vector3 playerPos;
    private Quaternion playerRot;
    private Vector3 enemyPos;
    private Quaternion enemyRot;
    public int score;
    public int enemyLives;
    const string fileName = "/mazeSave.dat";

    private void Awake()
    {
        if (pCtrl == null)
        {
            pCtrl = this;
            pCtrl.score = 0;
            pCtrl.enemyLives = 3;
            pCtrl.playerPos = this.transform.position;
            pCtrl.playerRot = this.transform.rotation;
            pCtrl.enemyPos = Enemy.transform.position;
            pCtrl.enemyRot = Enemy.transform.rotation;
            LoadState();
        }
    }
    void Start()
    {
        escMenu.enabled = false;
        this.transform.position = pCtrl.playerPos;
        this.transform.rotation = pCtrl.playerRot;
        enemy = Instantiate(Enemy, pCtrl.enemyPos, pCtrl.enemyRot);
        enemy.GetComponent<EnemyController>().lives = pCtrl.enemyLives;
        DontDestroyChildOnLoad(toggleText);
        DontDestroyChildOnLoad(scoreText);
        toggleText.enabled = false;
        scoreText.enabled = true;
        Walls = GameObject.FindGameObjectsWithTag("Walls");
        PongEntrance = GameObject.FindGameObjectWithTag("Pong");
        WinScreen.SetActive(false);
    }

    public static void DontDestroyChildOnLoad(Text child)
    {
        Transform parentTransform = child.transform;
        
        while( parentTransform.parent != null)
        {
            parentTransform = parentTransform.parent;
        }

        GameObject.DontDestroyOnLoad(parentTransform.gameObject);
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
                escMenu.enabled = false;
                Cursor.lockState = CursorLockMode.Locked;
            } else {
                Time.timeScale = 0;
                escMenu.enabled = true;
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
            this.transform.position = new Vector3(-13.72f, 0.27f, -13.53f);
            toggleText.enabled = false;
            scoreText.enabled = false;
            SaveState();
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
            pCtrl.enemyLives = data.enemyLives;
            pCtrl.playerPos[0] = data.playerPos[0];
            pCtrl.playerPos[1] = data.playerPos[1];
            pCtrl.playerPos[2] = data.playerPos[2];
            pCtrl.playerRot[0] = data.playerRot[0];
            pCtrl.playerRot[1] = data.playerRot[1];
            pCtrl.playerRot[2] = data.playerRot[2];
            pCtrl.playerRot[3] = data.playerRot[3];
            pCtrl.enemyPos[0] = data.enemyPos[0];
            pCtrl.enemyPos[1] = data.enemyPos[1];
            pCtrl.enemyPos[2] = data.enemyPos[2];
            pCtrl.enemyRot[0] = data.enemyRot[0];
            pCtrl.enemyRot[1] = data.enemyRot[1];
            pCtrl.enemyRot[2] = data.enemyRot[2];
            pCtrl.enemyRot[3] = data.enemyRot[3];
        }
    }

    public void SaveState()
    {
        pCtrl.playerPos = this.transform.position;
        pCtrl.playerRot = this.transform.rotation;
        pCtrl.enemyPos = enemy.transform.position;
        pCtrl.enemyRot = enemy.transform.rotation;
        pCtrl.enemyLives = enemy.GetComponent<EnemyController>().getLives();
        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = File.Open(Application.persistentDataPath + fileName, FileMode.OpenOrCreate);
        GameData data = new GameData();
        data.score = pCtrl.score;
        data.enemyLives = pCtrl.enemyLives;
        data.playerPos[0] = pCtrl.playerPos[0];
        data.playerPos[1] = pCtrl.playerPos[1];
        data.playerPos[2] = pCtrl.playerPos[2];
        data.playerRot[0] = pCtrl.playerRot[0];
        data.playerRot[1] = pCtrl.playerRot[1];
        data.playerRot[2] = pCtrl.playerRot[2];
        data.playerRot[3] = pCtrl.playerRot[3];
        data.enemyPos[0] = pCtrl.enemyPos[0];
        data.enemyPos[1] = pCtrl.enemyPos[1];
        data.enemyPos[2] = pCtrl.enemyPos[2];
        data.enemyRot[0] = pCtrl.enemyRot[0];
        data.enemyRot[1] = pCtrl.enemyRot[1];
        data.enemyRot[2] = pCtrl.enemyRot[2];
        data.enemyRot[3] = pCtrl.enemyRot[3];
        bf.Serialize(fs, data);
        fs.Close();
    }
}

[Serializable]
class GameData
{
    public int score;
    public int enemyLives;
    public float[] playerPos = new float[3];
    public float[] playerRot = new float[4];
    public float[] enemyPos = new float[3];
    public float[] enemyRot = new float[4];
}