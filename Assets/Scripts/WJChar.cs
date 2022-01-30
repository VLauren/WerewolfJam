using KrillAudio.Krilloud;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class WJChar : MonoBehaviour
{
    public static WJChar Instance { get; private set; }

    [Header("Movement")]
    [SerializeField] float DayMovementSpeed;
    [SerializeField] float NightMovementSpeed;
    [SerializeField] float GravityAccel = -1;
    [SerializeField] float RotationSpeed = 360;

    [Header("Attack")]
    [SerializeField] GameObject AttackArea;
    [SerializeField] float AttackDuration = 0.5f;
    [SerializeField] float AttackTimeRemaining = 0f;

    [Header("Dash")]
    [SerializeField] float DashDuration = 0.5f;
    [SerializeField] float DashSpeed = 20f;
    [SerializeField] float DashCooldown = 3f;
    
    [Header("Health")]
    public int MaxHP;
    public int CurrentHP;

    [Header("Effects")]
    [SerializeField] ParticleSystem FXInvulnerability;

    Vector3 moveInput;
    Vector3 controlMovement;

    protected Quaternion TargetRotation;
    protected float VerticalVelocity;

    protected Animator DayAnimator;
    protected Animator NightAnimator;

    protected bool Invulnerable;
    bool CanControl;
    bool CanDash;
    float DashTimeRemaining;
    float DashCooldownTimeRemaining;
    Vector3 DashDirection;

    protected Color[] OGDayColors;
    float colorOsc;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        CanControl = true;
        CanDash = true;
        CurrentHP = MaxHP;

        if (transform.Find("PersonajeRigeado"))
            DayAnimator = transform.Find("PersonajeRigeado").GetComponent<Animator>();
        if (transform.Find("WerewolfRigeado"))
            NightAnimator = transform.Find("WerewolfRigeado").GetComponent<Animator>();

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
        Dash();

        ColorFX();
    }

    void Walk()
    {
        if (!Dead && !CanControl) return;
        
        controlMovement = Vector3.zero;
        float moveAmount = (WJUtil.IsOnDaySide(transform.position) ? DayMovementSpeed : NightMovementSpeed);
        controlMovement = moveInput * Time.deltaTime * moveAmount;

        if (DayAnimator != null)
        {
            float newMS = Mathf.MoveTowards(DayAnimator.GetFloat("MovementSpeed"), moveInput.magnitude, Time.deltaTime * 10);
            DayAnimator.SetFloat("MovementSpeed", newMS);
        }
        if (NightAnimator != null)
        {
            float newMS = Mathf.MoveTowards(DayAnimator.GetFloat("MovementSpeed"), moveInput.magnitude, Time.deltaTime * 10);
            NightAnimator.SetFloat("MovementSpeed", newMS);
        }
    }

    void Rotation()
    {
        if (!Dead && !CanControl) return;
        
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
        if (!Dead && !CanControl) return;

        if (GetComponent<CharacterController>().isGrounded && VerticalVelocity < 0)
            VerticalVelocity = 0;
        VerticalVelocity += GravityAccel * Time.fixedDeltaTime;

        bool grounded = GetComponent<CharacterController>().isGrounded;

        GetComponent<CharacterController>().Move(controlMovement + new Vector3(0, VerticalVelocity, 0));
    }

    void Attack()
    {
        if (!Dead && !CanControl) return;

        if (AttackTimeRemaining > 0) {
            AttackTimeRemaining -= Time.deltaTime;
        } else {
            AttackTimeRemaining = 0f;
            AttackArea.gameObject.SetActive(false);
        }
    }

    void Dash()
    {
        if (DashTimeRemaining > 0)
        {
            DashTimeRemaining -= Time.deltaTime;

            GetComponent<CharacterController>().Move(moveInput * DashSpeed * Time.deltaTime);

        }
        else if (!CanControl)
        {
            DashTimeRemaining = 0f;
            DashCooldownTimeRemaining = DashCooldown;
            CanControl = true;
            CanDash = false;

            NightAnimator.SetTrigger("EndDash");

            Debug.Log("Can control again");
        }

        if (DashCooldownTimeRemaining > 0)
        {
            DashCooldownTimeRemaining -= Time.deltaTime;
        }
        else if (!CanDash)
        {
            DashCooldownTimeRemaining = 0f;
            CanDash = true;
            Debug.Log("Can dash again");
        }
    }

    void ColorFX()
    {
        if (!Invulnerable) {
            return;
        }

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
        if (!CanControl) return;
        moveInput = new Vector3(raw.x, 0, raw.y);
    }

    void OnFire(InputValue value)
    {
        // No ataco si no soy un hombre lobo
        if (WJUtil.IsOnDaySide(transform.position))
        {
            Debug.Log("Cant attack! You're not a wolf!");
            return;
        }

        if (AttackTimeRemaining > 0) {
            return;
        }

        StartCoroutine(WolfAttack());
    }

    IEnumerator WolfAttack()
    {
        AttackTimeRemaining = AttackDuration;

        NightAnimator.SetTrigger("Attack");

        WJGame.AudioSource.SetIntVar("lobo", 2);
        WJGame.AudioSource.Play("lobo");

        yield return new WaitForSeconds(0.1f);

        Vector3 FXPos = transform.Find("AtkFXPos").position;
        var fx = WJVisualFX.Effect(2, FXPos, Quaternion.Euler(0, -90, 0) * transform.rotation);
        fx.transform.parent = transform;

        WJRenderCam.CameraShake(0.05f, 0.15f);

        yield return new WaitForSeconds(0.1f);

        AttackArea.gameObject.SetActive(true);
    }

    void OnDash(InputValue value)
    {
        if (WJUtil.IsOnDaySide(transform.position))
        {
            return;
        }
        if (CanControl && CanDash)
        {
            // Inicio de dash

            DashDirection = moveInput;
            DashTimeRemaining = DashDuration;
            CanControl = false;

            // Rotacion del personaje
            transform.rotation = Quaternion.LookRotation(DashDirection, Vector3.up);
            NightAnimator.SetTrigger("Dash");

            WJRenderCam.CameraShake(0.15f, 0.15f);

            WJGame.AudioSource.SetIntVar("lobo", 4);
            WJGame.AudioSource.Play("lobo");
        }
    }

    internal void StartInvul()
    {
        Invulnerable = true;
        if (FXInvulnerability != null)
        {
            FXInvulnerability.Play();
        }
        else
        {
            Debug.Log("No hay partículas de invulnerabilidad");
        }

        WJGame.AudioSource.SetIntVar("lobo", 3);
        WJGame.AudioSource.Play("lobo");
    }

    internal void StopInvul()
    {
        Invulnerable = false;
        if (FXInvulnerability != null)
        {
            FXInvulnerability.Stop();
        }
    }

    public void ApplyDamage(int _damage)
    {
        if (Invulnerable || Dead)
            return;

        CurrentHP -= _damage;
        if (CurrentHP <= 0)
            Death();

        BroadcastMessage("Blink", SendMessageOptions.DontRequireReceiver);

        if (WJUtil.IsOnDaySide(transform.position))
        {
            WJGame.AudioSource.Play("girl");
        }
        else
        {
            WJGame.AudioSource.SetIntVar("lobo", 0);
            WJGame.AudioSource.Play("lobo");
        }

        WJRenderCam.CameraShake(0.15f, 0.2f);
    }

    public bool Dead = false;
    public virtual void Death()
    {
        WJGame.Death();

        Dead = true;
        CanControl = false;
        DayAnimator.SetTrigger("Death");
        NightAnimator.SetTrigger("Death");

        // Destroy(gameObject);
    }
}
