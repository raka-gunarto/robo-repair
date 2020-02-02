using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigRobotProgressManager : MonoBehaviour
{
    private int _progress = 0;

    public string[] bodyPartsProgression = {
        "Tracks",
        "Body",
        "Head",
        "Arms",
        "Main Cannon",
        "Arm Cannons",
        "Docking Door"
    };

    // Start is called before the first frame update
    void Start()
    {
        foreach(MeshRenderer mRender in gameObject.GetComponentsInChildren<MeshRenderer>()) 
            mRender.enabled = false;
    }

    // Called when correct part is supplied
    public void ProgressForward()
    {
        _progress += 1;
        foreach(MeshRenderer parentRenderer in gameObject.GetComponentsInChildren<MeshRenderer>(true))
        {
            if (parentRenderer.gameObject.name == bodyPartsProgression[_progress - 1])
            {
                foreach (MeshRenderer mRender in parentRenderer.GetComponentsInChildren<MeshRenderer>())
                    mRender.enabled = true;
                parentRenderer.GetComponent<MeshRenderer>().enabled = true;
                break;
            }
        }

        if (_progress == bodyPartsProgression.Length)
            gameObject.GetComponentInChildren<BoxCollider>(true).enabled = true;
    }

    public void ProgressBackward()
    {
        foreach (MeshRenderer parentRenderer in gameObject.GetComponentsInChildren<MeshRenderer>(true))
        {
            if (parentRenderer.gameObject.name == bodyPartsProgression[_progress - 1])
            {
                foreach (MeshRenderer mRender in parentRenderer.GetComponentsInChildren<MeshRenderer>())
                    mRender.enabled = false;
                parentRenderer.GetComponent<MeshRenderer>().enabled = false;
                break;
            }
        }

        _progress -= 1;
        gameObject.GetComponentInChildren<BoxCollider>(true).enabled = false;
    }
}
