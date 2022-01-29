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
    [SerializeField] float GravityAccel = -1;
    [SerializeField] float RotationSpeed = 360;
    [SerializeField] ParticleSystem FXInvulnerability;

    [Space]
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

    protected Animator DayAnimator;
    protected Animator NightAnimator;

    protected bool Invulnerable;

    protected Color[] OGDayColors;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        CurrentHP = MaxHP;

        if (transform.Find("PersonajeRigeado"))
            DayAnimator = transform.Find("PersonajeRigeado").GetComponent<Animator>();

        // Guardo los colores iniciales
        var mats = transform.Find("PersonajeRigeado/Character").GetComponent<Renderer>().materials;
        OGDayColors = new Color[mats.Length];
        for (int i = 0; i < mats.Length; i++)
            OGDayColors[i] = mats[i].color;
    }

    void Update()
    {
        Walk();
        Rotation();
        Gravity();
        Attack();

        ColorFX();
    }

    void Walk()
    {
        controlMovement = Vector3.zero;
        float moveAmount = (WJUtil.IsOnDaySide(transform.position) ? DayMovementSpeed : NightMovementSpeed);
        controlMovement = moveInput * Time.deltaTime * moveAmount;

        if (DayAnimator != null)
            DayAnimator.SetFloat("MovementSpeed", moveInput.magnitude);
        if (NightAnimator != null)
            NightAnimator.SetFloat("MovementSpeed", moveInput.magnitude);
    }

    void Rotation()
    {
        float rCam = Camera.main.transform.eulerAngles.y;
        // Direccion relativa a camara
        controlMovement = Quaternion.Euler(0, rCam, 0) * controlMovement;
        if (controlMovement != Vector3.zero)
        {
            TargetRotation = Quaternion.LookRotation(controlMovement, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, TargetRotation, Time.deltaTime * RotationSpeed);
        }
    }

    void Gravity()
    {
        if (GetComponent<CharacterController>().isGrounded && VerticalVelocity < 0)
            VerticalVelocity = 0;
        VerticalVelocity += GravityAccel * Time.fixedDeltaTime;

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

    float colorOsc;
    void ColorFX()
    {
        return;

        colorOsc = (Mathf.Sin(Time.time * 8 * Mathf.PI) + 1) / 2; // Oscila entre 0 y 1

        var mats = transform.Find("PersonajeRigeado/Character").GetComponent<Renderer>().materials;
        for (int i = 0; i < mats.Length; i++)
            mats[i].color = OGDayColors[i] + (Color.white / 5) * colorOsc;
        transform.Find("PersonajeRigeado/Character").GetComponent<Renderer>().materials = mats;
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
        // No ataco si no soy un hombre lobo
        if (WJUtil.IsOnDaySide(transform.position))
        {
            Debug.Log("Cant attack! You're not a wolf!");
            return;
        }

        // Debug.Log("Attack!");
        if (AttackTimeRemaining > 0) {
            return;
        }
        AttackTimeRemaining = AttackDuration;
        AttackArea.gameObject.SetActive(true);

        // foreach (var cosa in FindObjectsOfType<WJEnemy>())
            // cosa.ApplyDamage(30);
    }

    internal void StartInvul()
    {
        Invulnerable = true;
        if (FXInvulnerability != null) {
            FXInvulnerability.Play();
        } else {
            Debug.Log("No hay partículas de invulnerabilidad");
        }

        // TODO representar visualmente
    }

    internal void StopInvul()
    {
        Invulnerable = false;
    if (FXInvulnerability != null) {
                FXInvulnerability.Stop();
        }
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
