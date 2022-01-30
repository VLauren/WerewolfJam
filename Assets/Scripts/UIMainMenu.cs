using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class UIMainMenu : MonoBehaviour
{
    [SerializeField] GameObject mainMenuGroup;
    [SerializeField] GameObject controlsInfoGroup;

    bool showControlsInfo;

    void Start()
    {
        showControlsInfo = false;
        Debug.Log("Started");
    }

    void OnAnyKey(InputValue value)
    {
        Debug.Log("Starting the Game");
        if (!showControlsInfo) {
            mainMenuGroup.SetActive(false);
            controlsInfoGroup.SetActive(true);
            showControlsInfo = true;
        } else {
            SceneManager.LoadScene(1);
            // StartCoroutine(StartGame());
        }
    }

    IEnumerator StartGame()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(1);
    }
}
