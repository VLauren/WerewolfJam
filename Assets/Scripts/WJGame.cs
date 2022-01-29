using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WJGame : MonoBehaviour
{
    public static WJGame Instance { get; private set; }

    public static Material BMat;
    public Material BlinkMat;

    void Awake()
    {
        Instance = this;

        BMat = BlinkMat;
    }

    public static void Death()
    {
        Instance.StartCoroutine(Instance.DeathRoutine());
    }

    IEnumerator DeathRoutine()
    {
        yield return new WaitForSeconds(3);

        SceneManager.LoadScene(0);
    }
}
