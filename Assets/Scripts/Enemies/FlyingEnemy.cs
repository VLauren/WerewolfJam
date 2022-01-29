using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : WJEnemy
{
    public float MovementSpeed;

    void Update()
    {
        if (WJChar.Instance == null)
            return;

        transform.LookAt(WJChar.Instance.transform.position);

        transform.Translate(Vector3.forward * Time.deltaTime * MovementSpeed);

        if (Input.GetKeyDown(KeyCode.N))
            ApplyDamage(30);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<WJChar>() != null)
        {
            WJChar.Instance.ApplyDamage(10);

            Destroy(gameObject);
        }
    }

    public override void Death()
    {
        base.Death();

        Destroy(gameObject);
    }
}
