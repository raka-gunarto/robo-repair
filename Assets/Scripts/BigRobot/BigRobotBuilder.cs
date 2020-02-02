using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigRobotBuilder : MonoBehaviour
{
    public GameObject builder;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject != builder) return;

        // TODO: check it actually has the part
        gameObject.GetComponent<BigRobotProgressManager>().ProgressForward();
    }
}
