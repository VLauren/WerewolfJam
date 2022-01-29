using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [SerializeField] int damage = 10;
    public float Speed;

    float Elapsed = 0;

    void Start()
    {
        if (WJChar.Instance != null)
            transform.LookAt(WJChar.Instance.transform.position);
    }

    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * Speed);

        Elapsed += Time.deltaTime;
        if (Elapsed >= 5)
            Destroy(gameObject);

        transform.Find("Huesito").Rotate(Time.deltaTime * 360 * 3, 0, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<WJChar>() != null)
        {
            WJChar.Instance.ApplyDamage(damage);

            Destroy(gameObject);
        }
    }
}
