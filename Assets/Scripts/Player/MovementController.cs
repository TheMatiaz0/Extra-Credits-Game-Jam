using System;
using Cyberultimate.Unity;
using UnityEngine;

public class MovementController : MonoSingleton<MovementController>
{
    public float turnSpeed = 2.0f;
    public float moveSpeed = 8.0f;
    public float runSpeed = 12.0f;

    private bool isRunning = false;
 
    public float minTurnAngle = -90.0f;
    public float maxTurnAngle = 90.0f;

    public float jumpHeight = 20f;
    
    private float rotX;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        MouseAiming();
        KeyboardMovement();
        Jumping();
    }

    private void MouseAiming()
    {
        // get the mouse inputs
        var y = Input.GetAxis("Mouse X") * turnSpeed;
        Debug.Log(y);
        Debug.Log(transform.eulerAngles.y + y);
        rotX += Input.GetAxis("Mouse Y") * turnSpeed;
 
        // clamp the vertical rotation
        rotX = Mathf.Clamp(rotX, minTurnAngle, maxTurnAngle);
 
        // rotate the camera
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + y, 0);
        Camera.main.transform.eulerAngles = new Vector3(-rotX, Camera.main.transform.eulerAngles.y, 0);
    }

    private void KeyboardMovement()
    {
        var dir = new Vector3
        {
            x = Input.GetAxis("Horizontal"), 
            z = Input.GetAxis("Vertical")
        };

        isRunning = Input.GetKey(KeyCode.LeftShift);

        transform.Translate(dir * ((isRunning ? runSpeed : moveSpeed) * Time.deltaTime));
    }

    private void Jumping()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(new Vector3(0, jumpHeight * 100, 0));
        }
    }
}
