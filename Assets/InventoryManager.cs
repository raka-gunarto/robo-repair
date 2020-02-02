using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    
    public Transform ground;
    List<GameObject> inventory = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void add(GameObject obj){
        // obj.transform.setParent(transform);
        obj.transform.parent = transform;
        obj.transform.localPosition = new Vector3 (0,0,0);

        Vector3 diff = ((obj.GetComponent<Renderer>().bounds.size) * (inventory.Count + 2));
        obj.transform.localPosition = new Vector3(obj.transform.localPosition.x, diff.y, obj.transform.localPosition.z);
        inventory.Add(obj);
    }

    void remove(GameObject obj){
        obj.transform.parent = ground.transform;
        inventory.Remove(obj);
    }
}
