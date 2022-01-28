using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WJCam : MonoBehaviour
{
    public Vector3 Offset;

    void Update()
    {
        transform.position = WJChar.Instance.transform.position + Offset;
        transform.LookAt(WJChar.Instance.transform.position);
    }

}
