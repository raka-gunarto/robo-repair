using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleBobber : MonoBehaviour
{
    // Update is called once per frame
    private Vector3 LastTransform;
    private Vector3 TargetTransform;

    private float LerpFrac = 0.0f;

    private bool Up = true;

    public float BobbingSpeed = 4.0f;
    void Update()
    {
        if (transform.localPosition == TargetTransform)
        {
            LastTransform = transform.localPosition;
            if (Up)
                TargetTransform = new Vector3(transform.localPosition.x, transform.localPosition.y + 0.3f, transform.localPosition.z);
            else
                TargetTransform = new Vector3(transform.localPosition.x, transform.localPosition.y - 0.3f, transform.localPosition.z);
            Up = !Up;
            LerpFrac = 0.0f;
        }
        LerpFrac += BobbingSpeed * Time.deltaTime;
        transform.localPosition = Vector3.Lerp(LastTransform, TargetTransform, LerpFrac);
    }
}
