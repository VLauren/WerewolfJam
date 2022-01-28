using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WJCam : MonoBehaviour
{
    public const float OscSpeed = 0.1f;

    public enum CamType
    {
        RIGHT,
        LEFT
    }

    public Vector3 Offset;

    public float MagicNumber = 1;

    public CamType Type;

    float MaxFov;

    void Start()
    {
        MaxFov = GetComponent<Camera>().fieldOfView;
    }

    void Update()
    {
        transform.position = WJChar.Instance.transform.position + Offset;

        float xOffset;
        if (Type == CamType.LEFT)
            xOffset = 1 - Mathf.Abs(Mathf.Sin(Time.time * Mathf.PI * OscSpeed));
        else
            xOffset = Mathf.Abs(Mathf.Sin(Time.time * Mathf.PI * OscSpeed));

        // transform.LookAt(WJChar.Instance.transform.position + new Vector3(xOffset, 0, 0));
        // transform.LookAt(WJChar.Instance.transform.position - new Vector3(xOffset, 0, 0));
        transform.LookAt(WJChar.Instance.transform.position);

        if (Type == CamType.LEFT)
        {
            transform.position += new Vector3(-Mathf.Abs(Mathf.Sin(Time.time * Mathf.PI * OscSpeed)) * MagicNumber, 0, 0);
            GetComponent<Camera>().rect = new Rect(0, 0, xOffset, 1);
        }
        else
        {
            transform.position += new Vector3(MagicNumber - Mathf.Abs(Mathf.Sin(Time.time * Mathf.PI * OscSpeed)) * MagicNumber, 0, 0);
            GetComponent<Camera>().rect = new Rect(1 - xOffset, 0, xOffset, 1);
        }
    }

}
