using System;
using Cyberultimate;
using Cyberultimate.Unity;
using UnityEngine;

public class MovementController : MonoSingleton<MovementController>
{
    [Header("Mouse look")]
    public float mouseSensitivity = 100.0f;
    public float minTurnAngle = -90.0f;
    public float maxTurnAngle = 90.0f;
     
    [Header("Movement")]
    public float moveSpeed = 8.0f;
    public float runSpeed = 12.0f;
    public float movingStamina = 0.5f;
    public float runningStamina = 1f;
    
    private bool isRunning = false;
    private float CurrentSpeed => (isRunning ? runSpeed : moveSpeed);
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
    
    private float rotX;
    private Vector3 velocity;
    private CharacterController cc;


    private void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    private void Update()
    {
        MouseAiming();
        Running();
        KeyboardMovement();
        Jumping();
    }

    private void MouseAiming()
    {
        // get the mouse inputs
        var y = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        rotX += Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
 
        // clamp the vertical rotation
        rotX = Mathf.Clamp(rotX, minTurnAngle, maxTurnAngle);
 
        // rotate the camera
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + y, 0);
        Camera.main.transform.eulerAngles = new Vector3(-rotX, Camera.main.transform.eulerAngles.y, 0);
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
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            StaminaSystem.Instance.Stamina.TakeValue(Time.deltaTime * jumpingStamina, "Jumping");
        }
    }

    private void LosingStamina()
    {
        StaminaSystem.Instance.Stamina.TakeValue(Time.deltaTime * CurrentStamina, "Running");
    }
}
