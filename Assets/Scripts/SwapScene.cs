using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class SwapScene : MonoBehaviour
{
    PlayerController PlayerController;

    private void Start()
    {
        PlayerController = FindObjectOfType<PlayerController>();
    }
    public void LoadTheLevel(string theLevel)
    {
        SceneManager.LoadScene(theLevel);
    }

}
