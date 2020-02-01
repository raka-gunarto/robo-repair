using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    CharacterController characterController;

    public float moveSpeed = 5.0f;
    public float gravity = -9.81f;

    void Start()
    {
        print(transform.rotation);
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessMovementInputs();

        print("Controller " + controllerNumber + " VALUE: " + Input.GetAxis("Controller" + controllerNumber + "Trigger").ToString());
    }

    void ProcessMovementInputs()
    {
        float horizontalInput = Input.GetAxisRaw("Controller" + controllerNumber + "Horizontal");
        float verticalInput = Input.GetAxisRaw("Controller" + controllerNumber + "Vertical");

        ApplyTranslation(horizontalInput, verticalInput);
    }

    void ApplyTranslation(float horizontalInput, float verticalInput)
    {
        Vector3 movement = new Vector3(horizontalInput, ((characterController.isGrounded) ? 0 : (float)gravity * Time.deltaTime), verticalInput);
        Vector3 scaledMovement = movement.normalized * moveSpeed * Time.deltaTime;

        characterController.Move(scaledMovement);

        if (!(horizontalInput == 0 && verticalInput == 0))
            ApplyRotation(movement);
        
    }

    void ApplyRotation(Vector3 movement)
    {
        if (movement == new Vector3(0, 0, 0))
            return;
        movement.y = 0;
        Quaternion rot = new Quaternion();
        rot.SetLookRotation(movement);
        transform.rotation = rot;
    }
}
