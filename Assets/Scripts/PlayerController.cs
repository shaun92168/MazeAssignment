using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public CharacterController controller;
    public GameObject WinScreen;
    public float speed = 12f;
    public GameObject[] Walls;
    public Text toggleText;
    private bool toggleStatus;
    private IEnumerator coroutine;

    void Start()
    {
        toggleText.enabled = false;
        Walls = GameObject.FindGameObjectsWithTag("Walls");
        WinScreen.SetActive(false);
    }

    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        if(Input.GetKeyDown(KeyCode.Home))
        {
            SceneManager.LoadScene("SampleScene");
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            ToggleWalls();
        }

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
        }
        
        Debug.Log(col.name);
    }

    void ToggleWalls()
    {
        foreach (GameObject wall in Walls)
        {
            wall.gameObject.GetComponent<Collider>().enabled = !wall.gameObject.GetComponent<Collider>().enabled;
            toggleStatus = wall.gameObject.GetComponent<Collider>().enabled;
        }

        if(toggleStatus)
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
