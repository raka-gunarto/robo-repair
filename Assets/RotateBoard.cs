using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateBoard : MonoBehaviour
{

    public float rotationRate = 180;   

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, rotationRate * Time.deltaTime, 0);
    }
}
