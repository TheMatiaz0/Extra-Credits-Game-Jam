using System;
using Cyberultimate;
using Cyberultimate.Unity;
using UnityEngine;

public class MovementController : MonoSingleton<MovementController>
{   
    [Header("Movement")]
    public float moveSpeed = 8.0f;
    public float runSpeed = 12.0f;
    public float outOfStaminaSpeed = 5f;

    public float movingStamina = 0.5f;
    public float runningStamina = 1f;
    
    private bool isRunning = false;

    private bool IsOutOfStamina => GameManager.Instance.StaminaSys.Stamina.Value == 0;
    private float CurrentSpeed => IsOutOfStamina ? outOfStaminaSpeed : isRunning ? runSpeed : moveSpeed;
    private float CurrentStamina => (isRunning ? runningStamina : movingStamina);
    
    
    [Header("Gravity & jumping")]
    public float gravity = -9.8f;
    public float jumpHeight = 5f;
    public float jumpingStamina = 10f;

    [Header("Ground checks")]
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    private bool isGrounded;
    
    private Vector3 velocity;
    private CharacterController cc;

    public bool BlockMovement { get; set; }

    private bool step1 = false;
    private DateTime lastStepTime;

    private void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (BlockMovement)
		{
            return;
		}

        Running();
        KeyboardMovement();
        Jumping();
    }

    private void KeyboardMovement()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        
        var x = Input.GetAxis("Horizontal");
        var z = Input.GetAxis("Vertical");
        
        if(Math.Abs(x) > 0.2 || Math.Abs(z) > 0.2) LosingStamina();
        
        
        var dir = (transform.forward * z) + (transform.right * x);
        if (dir.magnitude > 1)
		{
            dir.Normalize();
        }

        if ((Mathf.Abs(x) > 0.5f || Mathf.Abs(z) > 0.5f) && lastStepTime < DateTime.Now.Subtract(TimeSpan.FromMilliseconds(500)))
        {
            AudioManager.Instance.PlaySFX(step1 ? "step1" : "step2");
            step1 = !step1;
            lastStepTime = DateTime.Now;
        }


        cc.Move(dir * (CurrentSpeed * Time.deltaTime));

        velocity.y += gravity * Time.deltaTime;
        cc.Move(velocity * Time.deltaTime);
    }

    private void Running()
    {
        isRunning = Input.GetKey(KeyCode.LeftShift);
    }

    private void Jumping()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && !IsOutOfStamina)
        {
            AudioManager.Instance.PlaySFX("jump");
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            GameManager.Instance.StaminaSys.Stamina.TakeValue(Time.deltaTime * jumpingStamina, "Jumping");
        }
    }

    private void LosingStamina()
    {
        GameManager.Instance.StaminaSys.Stamina.TakeValue(Time.deltaTime * CurrentStamina, "Running");
    }
}
