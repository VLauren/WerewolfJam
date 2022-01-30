using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WJUtil
{

    public static float OscValue()
    {
        return WJGame.LinePos;
    }

    public static bool IsOnDaySide(Vector3 _worldPosition)
    {
        Vector3 dir = Quaternion.Euler(0, WJUtil.OscValue() * -120 + 60, 0) * WJRenderCam.Instance.transform.forward;
        dir.y = 0;
        dir = Quaternion.Euler(0, 90, 0) * dir;

        Debug.DrawLine(WJRenderCam.Instance.transform.position, WJRenderCam.Instance.transform.position + dir * 30, Color.yellow);

        Plane plane = new Plane(dir, WJRenderCam.Instance.transform.position);


        return WJGame.InverseLine ? plane.GetSide(_worldPosition) : !plane.GetSide(_worldPosition);
    }
}
