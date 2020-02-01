using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    CharacterController characterController;

    public float moveSpeed = 5.0f;

    public enum ControllerType { WASD, ARROW_KEYS, CONTROLLER1, CONTROLLER2 };
    public ControllerType controller;

    private string xAxis;
    private string yAxis;
    private string trigger;

    void Start()
    {
        switch(controller)
        {
            case ControllerType.WASD:
                xAxis = "WASDHorizontal";
                yAxis = "WASDVertical";
                trigger = "WASDTrigger";
                break;
            case ControllerType.ARROW_KEYS:
                xAxis = "ArrowKeysHorizontal";
                yAxis = "ArrowKeysVertical";
                trigger = "ArrowKeysTrigger";
                break;
            case ControllerType.CONTROLLER1:
                xAxis = "Controller1Horizontal";
                yAxis = "Controller1Vertical";
                trigger = "Controller1Trigger";
                break;
            case ControllerType.CONTROLLER2:
                xAxis = "Controller2Horizontal";
                yAxis = "Controller2Vertical";
                trigger = "Controller2Trigger";
                break;
            default:
                break;
        }

        print(transform.rotation);
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessMovementInputs();

        print("Controller " + controller.ToString() + " VALUE: " + Input.GetAxis(trigger).ToString());
    }

    void ProcessMovementInputs()
    {
        float horizontalInput = Input.GetAxisRaw(xAxis);
        float verticalInput = Input.GetAxisRaw(yAxis);

        ApplyTranslation(horizontalInput, verticalInput);
    }

    void ApplyTranslation(float horizontalInput, float verticalInput)
    {
        Vector3 movement = new Vector3(horizontalInput, 0, verticalInput);
        Vector3 scaledMovement = movement.normalized;
        scaledMovement = movement * moveSpeed * Time.deltaTime;

        characterController.Move(scaledMovement);

        ApplyRotation(movement);
    }

    void ApplyRotation(Vector3 movement)
    {
        if (movement == new Vector3(0, 0, 0))
            return;
        print(Vector2.Angle(Vector2.up, new Vector2(movement.x, movement.z)));
        Quaternion rotation = Quaternion.Euler(0, Vector3.Angle(Vector3.forward, movement) * Mathf.Sign(Vector3.Dot(Vector3.right, movement)), 0);
        transform.rotation = rotation;
    }
}
