using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WJScoreKeeper : MonoBehaviour
{
    public static WJScoreKeeper Instance { get; private set; }
    int score;

    void Awake()
    {
        Instance = this;
    }

    public int GetScore()
    {
        return score;
    }

    public void ModifyScore(int value)
    {
        score += value;
        Mathf.Clamp(score, 0, int.MaxValue);
    }
}
