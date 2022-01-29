using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WJEnemy : MonoBehaviour
{
    public int MaxHP;
    public int CurrentHP;

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

        SendMessage("Blink");
    }

    public virtual void Death()
    {

    }
}
