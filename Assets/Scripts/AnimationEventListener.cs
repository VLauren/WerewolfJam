using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventListener : MonoBehaviour
{
    public enum StepType
    {
        GIRL,
        WOLF,
        SKELETON
    }

    [SerializeField] ParticleSystem effect;

    public StepType Type;

    void Step()
    {
        Debug.Log("Step");
        Instantiate(effect, transform.position, transform.rotation);

        WJGame.AudioSource.SetIntVar("pasos", (int)Type);
        WJGame.AudioSource.Play("step");
    }
}
