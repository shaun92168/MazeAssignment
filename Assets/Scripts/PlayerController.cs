using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public CharacterController controller;
    public GameObject WinScreen;
    public float speed = 12f; 
    void Start()
    {
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
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.name == "END")
        {
            WinScreen.SetActive(true);
        }
        
        Debug.Log(col.name);
    }
}
