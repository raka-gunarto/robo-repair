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
