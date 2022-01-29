using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RangedEnemy : WJEnemy
{
    public float MovementSpeed;
    public float Gravity;
    public float TargetDistance;

    [Space()]
    public float FireRate;
    public GameObject Projectile;

    protected NavMeshPath path;
    protected Quaternion TargetRotation;
    protected float VerticalVelocity;

    public override void Init()
    {
        base.Init();

        path = new NavMeshPath();

        StartCoroutine(Shooting()); // Anyways I started blasting
    }

    void Update()
    {
        if (WJChar.Instance == null)
            return;

        Vector3 _destination = WJChar.Instance.transform.position;

        NavMesh.CalculatePath(transform.position, _destination, NavMesh.AllAreas, path);

        for (int i = 0; i < path.corners.Length - 1; i++)
            Debug.DrawLine(path.corners[i], path.corners[i + 1], Color.green);

        Vector3 Movement;

        if (path.corners.Length > 1)
            Movement = (path.corners[1] - transform.position).normalized;
        else
            Movement = Vector3.zero;

        // Solo me acerco si estoy lejos
        if (Vector3.Distance(transform.position, WJChar.Instance.transform.position) > TargetDistance)
            GetComponent<CharacterController>().Move(Movement * Time.deltaTime * MovementSpeed);

        // Rotation
        if (Movement != Vector3.zero)
        {
            TargetRotation = Quaternion.LookRotation(Movement, Vector3.up);
            TargetRotation = Quaternion.Euler(0, TargetRotation.eulerAngles.y, 0);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, TargetRotation, Time.deltaTime * 180);
        }

        // Gravedad
        if (GetComponent<CharacterController>().isGrounded && VerticalVelocity < 0)
            VerticalVelocity = 0;
        VerticalVelocity += Gravity * Time.fixedDeltaTime;
        GetComponent<CharacterController>().Move(new Vector3(0, VerticalVelocity, 0));

        if (Input.GetKeyDown(KeyCode.N))
            ApplyDamage(30);
    }

    IEnumerator Shooting()
    {
        while(true)
        {
            yield return new WaitForSeconds(1.0f / FireRate);
            Instantiate(Projectile, transform.position + transform.forward, Quaternion.identity);
        }
    }

    public override void Death()
    {
        base.Death();

        Destroy(gameObject);
    }
}
