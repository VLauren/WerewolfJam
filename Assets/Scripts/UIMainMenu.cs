using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class UIMainMenu : MonoBehaviour
{
    void Start()
    {
        Debug.Log("Started");
    }

    void OnAnyKey(InputValue value)
    {
        Debug.Log("Starting the Game");
        SceneManager.LoadScene(1);
    }
}
