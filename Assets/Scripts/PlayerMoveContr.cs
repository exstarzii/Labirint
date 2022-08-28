using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(CharacterController))]
public class PlayerMoveContr : MonoBehaviour
{
	public float speed = 6.0f;//общая скорость перемещения
	public float jumpSpeed = 2.0f;//скорость прыжка
	public float gravity = 2.0f;//скорость падения

	public float cameraSpeed=17;

	public Camera cam;
	private Vector3 rotationDirection;


	private Vector3 moveDirection;//отвечает за направление движения в следущем кадре
	private CharacterController controller;

	void Start()
	{
		controller = GetComponent<CharacterController>();
		rotationDirection = Vector3.zero;
	}

	void Update()
    {
        moveDirection.x = Input.GetAxis("Horizontal");
        if (controller.isGrounded)
        {
            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpSpeed;
            }
        }
        moveDirection.y = moveDirection.y - gravity * Time.deltaTime;
        moveDirection.z = Input.GetAxis("Vertical");
        moveDirection = transform.TransformDirection(moveDirection);
        controller.Move(moveDirection * Time.deltaTime * speed);
        MouseMove();
    }


    void MouseMove()
    {
        controller.transform.Rotate(0, Input.GetAxis("Mouse X") * Time.deltaTime * cameraSpeed, 0);
        rotationDirection.x -= Input.GetAxis("Mouse Y") * Time.deltaTime * cameraSpeed;
        Mathf.Clamp(rotationDirection.x, -90, 90);
        cam.transform.localEulerAngles = rotationDirection;
    }

}