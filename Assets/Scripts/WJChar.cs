using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WJChar : MonoBehaviour
{
    public static WJChar Instance { get; private set; }

    public float DayMovementSpeed;
    public float NightMovementSpeed;

    [Space()]
    public int MaxHP;
    public int CurrentHP;

    protected Quaternion TargetRotation;
    protected float VerticalVelocity;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        CurrentHP = MaxHP;
    }

    void Update()
    {
        Vector3 controlMovement = Vector3.zero;
        float fMove = Input.GetAxisRaw("Vertical");
        float lMove = Input.GetAxisRaw("Horizontal");

        controlMovement = new Vector3(lMove, 0, fMove) * Time.deltaTime * (WJUtil.IsOnDaySide(transform.position) ? DayMovementSpeed : NightMovementSpeed);

        float rCam = Camera.main.transform.eulerAngles.y;

        // Direccion relativa a camara
        controlMovement = Quaternion.Euler(0, rCam, 0) * controlMovement;

        // Rotacion
        if (controlMovement != Vector3.zero)
        {
            TargetRotation = Quaternion.LookRotation(controlMovement, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, TargetRotation, Time.deltaTime * 360);
        }

        // Gravedad
        if (GetComponent<CharacterController>().isGrounded && VerticalVelocity < 0)
            VerticalVelocity = 0;
        VerticalVelocity += -1 * Time.fixedDeltaTime; // TODO ese 1, datos gravedad

        bool grounded = GetComponent<CharacterController>().isGrounded;

        GetComponent<CharacterController>().Move(controlMovement + new Vector3(0, VerticalVelocity, 0));

        // TODO: var de movement speed, var de gravity, var de model rotation speed
    }

    public void ApplyDamage(int _damage)
    {
        CurrentHP -= _damage;
        if (CurrentHP <= 0)
            Death();

        BroadcastMessage("Blink");
    }

    public virtual void Death()
    {
        WJGame.Death();
        Destroy(gameObject);
    }

}
