using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WJGame : MonoBehaviour
{
    public static WJGame Instance { get; private set; }

    public static Material BMat;
    public Material BlinkMat;
    
    [Space()]
    public float InvulTime;

    internal int InvulGauge = 0; 

    bool InvulActive;

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

    public static void AddInvul(int _invul)
    {
        if (Instance.InvulActive)
            return;

        Instance.InvulGauge += _invul;
        print("Invul gauge: " + Instance.InvulGauge);

        if(Instance.InvulGauge >= 100)
        {
            Instance.InvulGauge = 0;
            Instance.StartCoroutine(Instance.InvulRoutine());
        }
    }


    IEnumerator InvulRoutine()
    {
        print("START INVUL");
        InvulActive = true;
        WJChar.Instance.StartInvul();

        yield return new WaitForSeconds(InvulTime);

        InvulActive = false;
        WJChar.Instance.StopInvul();
        print("END INVUL");
    }
}
