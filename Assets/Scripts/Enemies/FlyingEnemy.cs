using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : WJEnemy
{
    public float MovementSpeed;

    void Update()
    {
        transform.LookAt(WJChar.Instance.transform.position);

        transform.Translate(Vector3.forward * Time.deltaTime * MovementSpeed);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<WJChar>() != null)
        {
            Destroy(gameObject);
            print("HIT PLAYER!");
        }
    }
}
