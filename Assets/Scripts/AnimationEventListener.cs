using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventListener : MonoBehaviour
{
    [SerializeField] ParticleSystem effect;

    void Step()
    {
        Debug.Log("Step");
        Instantiate(effect, transform.position, transform.rotation);
    }
}
