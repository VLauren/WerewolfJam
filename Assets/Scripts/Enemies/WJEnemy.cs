using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WJEnemy : MonoBehaviour
{
    public int MaxHP;
    public int CurrentHP;

    [Space]
    public int InvulReward;

    private void Start()
    {
        Init();
    }

    public virtual void Init()
    {
        CurrentHP = MaxHP;
    }

    public virtual void ApplyDamage(int _damage)
    {
        CurrentHP -= _damage;
        if (CurrentHP <= 0)
            Death();

        BroadcastMessage("Blink", SendMessageOptions.DontRequireReceiver);
    }

    public virtual void Death()
    {
        WJScoreKeeper.Instance.ModifyScore(InvulReward);
        WJGame.AddInvul(InvulReward);
    }
}
