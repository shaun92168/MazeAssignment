using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System;

public class Reset : MonoBehaviour
{
    PlayerController PlayerController;
    void Start()
    {
        PlayerController = FindObjectOfType<PlayerController>();
    }

    public void resetScene()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Destroy(PlayerController.scoreText);
        Destroy(PlayerController.toggleText);
        try
        {
            File.Delete(Application.persistentDataPath + "/mazeSave.dat");
        } catch (Exception ex)
        {
            Debug.LogException(ex);
        }
        SceneManager.LoadScene("Maze");
        Time.timeScale = 1;
    }
}
