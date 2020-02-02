using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    CharacterController characterController;

    public float moveSpeed = 15f;
    public float accelerationRate = 2f;
    public float decelerationFactor = 1.2f;
    public Boolean inertial = true;

    private Vector3 currentSpeed = new Vector3(0.0f, 0.0f, 0.0f);

    public enum ControllerType { WASD, ARROW_KEYS, CONTROLLER1, CONTROLLER2 };
    public ControllerType controller;

    private string xAxis;
    private string yAxis;
    private string trigger;

    public float miningRadius = 3f;
    private bool isMining = false;
    private GameObject target;
    private GameObject guiElement;
    private GUIHover progress;

    private Sprite progressCircle;

    public float gravity = -100f;

    void Start()
    {
        progressCircle = Resources.Load<Sprite>("ProgressCircle");

        switch (controller)
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

        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!isMining)
        {
            ProcessMovementInputs();
        }

        if (Input.GetAxis(trigger) == 1 && !isMining)
        {
            target = getClosestMineable();
            if (target)
            {

                isMining = true;
                moveSpeed = 0;

                guiElement = new GameObject("Target");
                guiElement.transform.SetParent(GameObject.Find("Canvas").transform);
                ProgressCircle progressCircleComponent = guiElement.AddComponent<ProgressCircle>();
                progressCircleComponent.owner = this;
                guiElement.transform.localScale = new Vector3(0.2f, 0.2f, 1);
                Image image = guiElement.AddComponent<Image>();
                image.fillAmount = 0;
                image.sprite = progressCircle;
                image.type = Image.Type.Filled;
                image.fillOrigin = 2;
                
                progress = target.AddComponent<GUIHover>();
                progress.image = image;
            }
        }
        else if(isMining && Input.GetAxis(trigger) != 1)
        {
            isMining = false;
            Destroy(guiElement);
            Destroy(progress);
        }
        
    }

    public void finishMining()
    {
        isMining = false;
        Destroy(guiElement);
        Destroy(progress);

        GameObject drop = Instantiate(target.GetComponent<Mineable>().drops);

        drop.GetComponent<Rigidbody>().detectCollisions = false;

        Destroy(target);

        GetComponent<InventoryManager>().add(drop);
    }

    GameObject getClosestMineable()
    {
        Collider[] hitColliders = Physics.OverlapSphere(gameObject.transform.position, miningRadius);
        for(int i = 0; i < hitColliders.Length; i++)
        {
            if(hitColliders[i].GetComponent<Mineable>())
            {
                return hitColliders[i].gameObject;
            }
        }

        return null;
    }

    void ProcessMovementInputs()
    {
        float horizontalInput = Input.GetAxisRaw(xAxis);
        float verticalInput = Input.GetAxisRaw(yAxis);

        ApplyTranslation(horizontalInput, verticalInput);
    }

    void ApplyTranslation(float horizontalInput, float verticalInput)
    {

        Vector3 appliedSpeed = new Vector3(moveSpeed, moveSpeed, moveSpeed);
        if (inertial)
        {
            appliedSpeed = new Vector3((float)(currentSpeed.x + accelerationRate * Time.deltaTime * (horizontalInput) - (currentSpeed.x * decelerationFactor * accelerationRate * Time.deltaTime)),
                ((characterController.isGrounded) ? 0 : (float)gravity * Time.deltaTime),
                (float)(currentSpeed.z + accelerationRate * Time.deltaTime * verticalInput - (currentSpeed.z * decelerationFactor * accelerationRate * Time.deltaTime)));
            currentSpeed = appliedSpeed;


            Vector3 scaledMovement = appliedSpeed;

            characterController.Move(scaledMovement);
            if (!(horizontalInput == 0 && verticalInput == 0))
                ApplyRotation(scaledMovement);
        }
        else
        {
            Vector3 movement = new Vector3(horizontalInput, ((characterController.isGrounded) ? 0 : (float)gravity * Time.deltaTime), verticalInput);
            Vector3 scaledMovement = movement.normalized * moveSpeed * Time.deltaTime;

            characterController.Move(scaledMovement);
            if (!(horizontalInput == 0 && verticalInput == 0))
                ApplyRotation(movement);
        }

        if(transform.position.y < -30)
        {
            transform.position = new Vector3(5f, 20f, 5f);
        }

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

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.name.Contains("Player"))
        {
            hit.gameObject.GetComponent<InventoryManager>().dropAll();
            GetComponent<InventoryManager>().dropAll();
        }

        else if (hit.gameObject.GetComponent<Pickupable>() &&
            hit.gameObject.GetComponent<Pickupable>().enabled &&
            Vector3.Magnitude(hit.rigidbody.velocity) < 1)
        {
            hit.gameObject.GetComponent<Pickupable>().enabled = false;

            hit.rigidbody.isKinematic       = true;
            hit.rigidbody.detectCollisions  = false;
            hit.rigidbody.useGravity        = false;

            GetComponent<InventoryManager>().add(hit.gameObject);
        }
    }

    public Vector3 getVelocity()
    {
        return this.currentSpeed;
    }
}
