using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WJRenderCam : MonoBehaviour
{
    public static WJRenderCam Instance { get; private set; }

    public const float OscSpeed = 0.1f;

    public float MiddleZ;
    public float ZMovementMultiplier;

    [Space]
    public float MaxX;
    public float MinX;

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

        Vector3 pointToFollow = WJChar.Instance.transform.position;
        pointToFollow.z = MiddleZ + (WJChar.Instance.transform.position.z - MiddleZ) * ZMovementMultiplier;

        transform.position = pointToFollow + Offset;
        transform.LookAt(pointToFollow);

        if (Type == CamType.RIGHT)
        {
            Vector3 dir = transform.forward;
            Debug.DrawLine(transform.position, transform.position + dir * 30, Color.yellow);

            dir = Quaternion.Euler(0, WJUtil.OscValue() * -120 + 60, 0) * dir;
            Debug.DrawLine(transform.position, transform.position + dir * 30, Color.blue);
        }

        if (transform.position.x < MinX)
            transform.position = new Vector3(MinX, transform.position.y, transform.position.z);
        if (transform.position.x > MaxX)
            transform.position = new Vector3(MaxX, transform.position.y, transform.position.z);
    }

}
