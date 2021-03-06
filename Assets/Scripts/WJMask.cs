using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WJMask : MonoBehaviour
{
    public const float OscSpeed = 0.1f;

    public enum MaskType
    {
        RIGHT,
        LEFT
    }

    public MaskType Type;

    public float StartWidth;

    private void Awake()
    {
        StartWidth = Screen.width;
    }

    void Update()
    {
        StartWidth = Screen.width;

        var child1 = GetComponent<RectTransform>().GetChild(1);
        var child2 = GetComponent<RectTransform>().GetChild(0);
        child1.parent = child1.parent.parent;
        child2.parent = child2.parent.parent;

        float xScale;

        if (Type == MaskType.RIGHT && !WJGame.InverseLine || Type == MaskType.LEFT && WJGame.InverseLine)
            xScale = -1 + WJGame.LinePos;
        else
            xScale = - WJGame.LinePos;

        Rect r = GetComponent<RectTransform>().rect;

        GetComponent<RectTransform>().sizeDelta = new Vector2(xScale * StartWidth, 0);

        // if (Type == MaskType.RIGHT)
        if (Type == MaskType.RIGHT && !WJGame.InverseLine || Type == MaskType.LEFT && WJGame.InverseLine)
            GetComponent<RectTransform>().anchoredPosition = new Vector2(- xScale * StartWidth / 2, 0);
        else
            GetComponent<RectTransform>().anchoredPosition = new Vector2(xScale * StartWidth / 2, 0);

        // GetComponent<RectTransform>().sizeDelta = new Vector2(- Mathf.Abs(Mathf.Sin(Time.time * Mathf.PI * OscSpeed)) * StartWidth, 0);

        child2.parent = GetComponent<RectTransform>();
        child1.parent = GetComponent<RectTransform>();
    }
}
