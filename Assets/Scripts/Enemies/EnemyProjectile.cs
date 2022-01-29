using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public float Speed;

    float Elapsed = 0;

    void Start()
    {
        transform.LookAt(WJChar.Instance.transform.position);
    }

    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * Speed);

        Elapsed += Time.deltaTime;
        if (Elapsed >= 5)
            Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<WJChar>() != null)
        {
            Destroy(gameObject);
            print("HIT PLAYER!");
        }
    }
}
