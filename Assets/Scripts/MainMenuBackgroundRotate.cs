using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuBackgroundRotate : MonoBehaviour
{
    public float RotationSpeed = 3.0f;
    void Update()
    {
        transform.Rotate(0, RotationSpeed * Time.deltaTime, 0);
    }
}
