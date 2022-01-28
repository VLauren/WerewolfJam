using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WJRenderCam : MonoBehaviour
{
    public const float OscSpeed = 0.1f;

    public enum CamType
    {
        RIGHT,
        LEFT
    }

    public Vector3 Offset;

    public CamType Type;

    void Update()
    {
        transform.position = WJChar.Instance.transform.position + Offset;

        transform.LookAt(WJChar.Instance.transform.position);
    }

}
