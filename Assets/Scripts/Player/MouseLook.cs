using Cyberultimate.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MouseLook : MonoSingleton<MouseLook>
{
    [SerializeField]
    private int startMouseSensitivity = 90;

    public float MouseSensitivity { get; set; } = 350;

    [SerializeField]
    private float minTurnAngle = -90;

    [SerializeField]
    private float maxTurnAngle = 90f;

    private float xRotation = 0f;

    [SerializeField]
    private Transform playerBody = null;

    public bool BlockAiming { get; set; }

	protected void Start()
	{
        MouseSensitivity = startMouseSensitivity;
	}


	protected void Update()
    {
        if (BlockAiming)
		{
            return;
		}
        
        var mouseX = Input.GetAxis("Mouse X") * MouseSensitivity * Time.deltaTime;
        var mouseY = Input.GetAxis("Mouse Y") * MouseSensitivity * Time.deltaTime;


        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, minTurnAngle, maxTurnAngle);

        transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
