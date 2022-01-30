using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitbox : MonoBehaviour
{
    public int Damage;

    void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<WJEnemy>() != null)
        {
            other.GetComponent<WJEnemy>().ApplyDamage(Damage);

            WJVisualFX.Effect(3, transform.position);

            WJGame.AudioSource.SetIntVar("lobo", 1);
            WJGame.AudioSource.Play("lobo");
        }
    }
}
