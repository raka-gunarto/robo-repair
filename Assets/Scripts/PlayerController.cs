using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    CharacterController characterController;

    public float moveSpeed = 5.0f;
    void Start()
    {
        print(transform.rotation);
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessMovementInputs();
    }

    void ProcessMovementInputs()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        ApplyTranslation(horizontalInput, verticalInput);
    }

    void ApplyTranslation(float horizontalInput, float verticalInput)
    {
        if (horizontalInput == 0 && verticalInput == 0)
            return;

        Vector3 movement = new Vector3(horizontalInput, ((characterController.isGrounded) ? 0 : (float)-9.81 * Time.deltaTime), verticalInput);
        Vector3 scaledMovement = movement.normalized;
        scaledMovement = movement * moveSpeed * Time.deltaTime;

        characterController.Move(scaledMovement);

        ApplyRotation(movement);
    }

    void ApplyRotation(Vector3 movement)
    {
        if (movement == new Vector3(0, 0, 0))
            return;
        Quaternion rot = new Quaternion();
        rot.SetLookRotation(movement);
        transform.rotation = rot;
    }
}
