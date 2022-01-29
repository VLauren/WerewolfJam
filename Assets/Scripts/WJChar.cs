using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class WJChar : MonoBehaviour
{
    public static WJChar Instance { get; private set; }

    [SerializeField] float DayMovementSpeed;
    [SerializeField] float NightMovementSpeed;
    [SerializeField] GameObject AttackArea;
    [SerializeField] float AttackDuration = 0.5f;
    [SerializeField] float AttackTimeRemaining = 0f;

    Vector3 moveInput;
    Vector3 controlMovement;
    [Space()]
    public int MaxHP;
    public int CurrentHP;

    protected Quaternion TargetRotation;
    protected float VerticalVelocity;

    protected bool Invulnerable; 

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
        Walk();
        Rotation();
        Gravity();
        Attack();
        // TODO: var de movement speed, var de gravity, var de model rotation speed
    }

    void Walk()
    {
        controlMovement = Vector3.zero;
        float moveAmount = (WJUtil.IsOnDaySide(transform.position) ? DayMovementSpeed : NightMovementSpeed);
        controlMovement = moveInput * Time.deltaTime * moveAmount;
    }

    void Rotation()
    {
        float rCam = Camera.main.transform.eulerAngles.y;
        // Direccion relativa a camara
        controlMovement = Quaternion.Euler(0, rCam, 0) * controlMovement;
        if (controlMovement != Vector3.zero)
        {
            TargetRotation = Quaternion.LookRotation(controlMovement, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, TargetRotation, Time.deltaTime * 360);
        }
    }

    void Gravity()
    {
        if (GetComponent<CharacterController>().isGrounded && VerticalVelocity < 0)
            VerticalVelocity = 0;
        VerticalVelocity += -1 * Time.fixedDeltaTime; // TODO ese 1, datos gravedad

        bool grounded = GetComponent<CharacterController>().isGrounded;

        GetComponent<CharacterController>().Move(controlMovement + new Vector3(0, VerticalVelocity, 0));
    }

    void Attack()
    {
        if (AttackTimeRemaining > 0) {
            AttackTimeRemaining -= Time.deltaTime;
        } else {
            AttackTimeRemaining = 0f;
            AttackArea.gameObject.SetActive(false);
        }
    }

    void OnMove(InputValue value)
    {
        Vector2 raw = value.Get<Vector2>();
        moveInput = Vector2.zero;
        moveInput = new Vector3(raw.x, 0, raw.y);
        // Debug.Log(moveInput);
    }

    void OnFire(InputValue value)
    {
        Debug.Log("Attack!");
        if (AttackTimeRemaining > 0) {
            return;
        }
        AttackTimeRemaining = AttackDuration;
        AttackArea.gameObject.SetActive(true);
    }

    internal void StartInvul()
    {
        Invulnerable = true;

        // TODO representar visualmente
    }

    internal void StopInvul()
    {
        Invulnerable = false;

        // TODO representar visualmente
    }

    public void ApplyDamage(int _damage)
    {
        if (Invulnerable)
            return;

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
