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
        if (Type == StepType.GIRL && WJUtil.IsOnDaySide(WJChar.Instance.transform.position))
        {
            WJGame.AudioSource.SetIntVar("pasos", 0);
            WJGame.AudioSource.Play("pasos");
            if (effect != null)
                Instantiate(effect, transform.position, transform.rotation);
        }
        if (Type == StepType.WOLF && !WJUtil.IsOnDaySide(WJChar.Instance.transform.position))
        {
            WJGame.AudioSource.SetIntVar("pasos", 1);
            WJGame.AudioSource.Play("pasos");
            if (effect != null)
                Instantiate(effect, transform.position, transform.rotation);
        }
        if (Type == StepType.SKELETON)
        {
            WJGame.AudioSource.SetIntVar("pasos", 2);
            WJGame.AudioSource.Play("pasos");
            if (effect != null)
                Instantiate(effect, transform.position, transform.rotation);
        }
    }
}
