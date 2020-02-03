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

    public Dictionary<string, int>[] partsRequirementsProgression =
    {
        new Dictionary<string, int>()
        {
            {"Iron", 2 },
            {"Copper", 2 },
            {"Wood", 2 }
        },
        new Dictionary<string, int>()
        {
            {"Iron", 3 },
            {"Copper", 1 },
            {"Wood", 0 }
        },
        new Dictionary<string, int>()
        {
            {"Iron", 2 },
            {"Copper", 0 },
            {"Wood", 0 }
        },
        new Dictionary<string, int>()
        {
            {"Iron", 0 },
            {"Copper", 2 },
            {"Wood", 0 }
        },
        new Dictionary<string, int>()
        {
            {"Iron", 2 },
            {"Copper", 2 },
            {"Wood", 2 }
        },
        new Dictionary<string, int>()
        {
            {"Iron", 0 },
            {"Copper", 2 },
            {"Wood", 4 }
        },
        new Dictionary<string, int>()
        {
            {"Iron", 1 },
            {"Copper", 1 },
            {"Wood", 2 }
        }
    };

    void Start()
    {
        // Disable rendering
        foreach(MeshRenderer mRender in gameObject.GetComponentsInChildren<MeshRenderer>()) 
            mRender.enabled = false;

        // Disable collision
        foreach (MeshCollider mCollider in gameObject.GetComponentsInChildren<MeshCollider>())
            mCollider.enabled = false;
    }

    public int GetProgress() { return _progress;  }

    // Called when correct part is supplied
    public void ProgressForward()
    {
        GetComponent<BigRobotBuilder>().progress();

        _progress += 1;
        foreach(MeshRenderer parentRenderer in gameObject.GetComponentsInChildren<MeshRenderer>(true))
        {
            if (parentRenderer.gameObject.name == bodyPartsProgression[_progress - 1])
            {
                foreach (MeshRenderer mRender in parentRenderer.GetComponentsInChildren<MeshRenderer>())
                    mRender.enabled = true;

                parentRenderer.GetComponent<MeshCollider>().enabled = true;
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

                parentRenderer.GetComponent<MeshCollider>().enabled = false;
                parentRenderer.GetComponent<MeshRenderer>().enabled = false;
                break;
            }
        }

        _progress -= 1;
        gameObject.GetComponentInChildren<BoxCollider>(true).enabled = false;
    }
}
