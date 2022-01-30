using KrillAudio.Krilloud;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WJGame : MonoBehaviour
{
    public static WJGame Instance { get; private set; }

    public static Material BMat;
    public Material BlinkMat;

    public static KLAudioSource AudioSource;

    public static float LinePos { get; private set; }
    public static bool InverseLine { get; private set; }
    
    [Space()]
    public float InvulTime;

    internal int InvulGauge = 0; 

    bool InvulActive;

    [Space()]
    public float LineOscSpeed;
    public float LineOscSpeedDay;
    public float LineOscSpeedNight;
    public bool ForceLineVal;
    [Range(0, 1)]
    public float ForcedLinePos;
    public bool ForceLineInverse;

    float LineTime = 0;

    void Update()
    {
        if (ForceLineVal)
        {
            LinePos = ForcedLinePos;
            InverseLine = ForceLineInverse;
        }
        else
        {
            if (WJUtil.IsOnDaySide(WJChar.Instance.transform.position))
                LineTime += Time.deltaTime * LineOscSpeedDay;
            else
                LineTime += Time.deltaTime * LineOscSpeedNight;

            if(!InverseLine)
            {
                // LinePos = Mathf.Abs(Mathf.Sin(LineTime * Mathf.PI * LineOscSpeed));
                LinePos = (1 - Mathf.Cos(LineTime * Mathf.PI * LineOscSpeed)) / 2;
                Debug.Log((LineTime * LineOscSpeed) + " - " + LineTime + " - " + LinePos);

                if((LineTime * LineOscSpeed) > 1f)
                {
                    InverseLine = true;
                    LineTime = 0;
                    LinePos = (1 - Mathf.Cos(LineTime * Mathf.PI * LineOscSpeed)) / 2;
                }

                WJGame.AudioSource.SetFloatVar("volmusicadia", Mathf.Clamp((LinePos - 1f/3) * 3, 0.0f, 1.0f));
                WJGame.AudioSource.SetFloatVar("volmusicanoche", Mathf.Clamp(((1 - LinePos) - 1f / 3) * 3, 0.0f, 1.0f));
            }
            else
            {
                // LinePos = Mathf.Abs(Mathf.Sin(LineTime * Mathf.PI * LineOscSpeed));
                LinePos = (1 - Mathf.Cos(LineTime * Mathf.PI * LineOscSpeed)) / 2;
                Debug.Log((LineTime * LineOscSpeed) + " - " + LineTime + " - " + LinePos);

                if((LineTime * LineOscSpeed) > 1f)
                {
                    InverseLine = false;
                    LineTime = 0;
                    LinePos = (1 - Mathf.Cos(LineTime * Mathf.PI * LineOscSpeed)) / 2;
                }

                WJGame.AudioSource.SetFloatVar("volmusicanoche", Mathf.Clamp((LinePos - 1f/3) * 3, 0.0f, 1.0f));
                WJGame.AudioSource.SetFloatVar("volmusicadia", Mathf.Clamp(((1 - LinePos) - 1f / 3) * 3, 0.0f, 1.0f));
            }
        }
    }

    void Awake()
    {
        Instance = this;

        BMat = BlinkMat;

        AudioSource = GetComponent<KLAudioSource>();
    }

    private void Start()
    {
        WJGame.AudioSource.Play("musica");
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
