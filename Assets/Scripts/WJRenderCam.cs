using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WJRenderCam : MonoBehaviour
{
    public static WJRenderCam Instance { get; private set; }

    public const float OscSpeed = 0.1f;

    public enum CamType
    {
        RIGHT,
        LEFT
    }

    public Vector3 Offset;

    public CamType Type;

    void Awake()
    {
        if (Type == CamType.RIGHT)
            Instance = this;
    }

    void Update()
    {
        if (WJChar.Instance == null)
            return;

        transform.position = WJChar.Instance.transform.position + Offset;
        transform.LookAt(WJChar.Instance.transform.position);

        if (Type == CamType.RIGHT)
        {
            Vector3 dir = transform.forward;
            Debug.DrawLine(transform.position, transform.position + dir * 30, Color.yellow);

            dir = Quaternion.Euler(0, WJUtil.OscValue() * -120 + 60, 0) * dir;
            Debug.DrawLine(transform.position, transform.position + dir * 30, Color.blue);
        }
    }

}
