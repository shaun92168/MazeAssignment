using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveButton : MonoBehaviour
{
    PlayerController PlayerController;

    private void Start()
    {
        PlayerController = FindObjectOfType<PlayerController>();
    }

    public void SaveState()
    {
        PlayerController.SaveState();
    }
}
