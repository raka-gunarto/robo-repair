using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{

    public float explosiveness = 12;

    public Transform ground;
    public List<GameObject> inventory = new List<GameObject>();
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
        obj.transform.position = transform.position;
        obj.transform.localPosition = new Vector3 (0,0,0);

        //Vector3 size = obj.GetComponent<Renderer>().bounds.size;
        Vector3 size = new Vector3(1f, 1f, 1f);
        Vector3 diff = (size * (inventory.Count + 2));
        obj.transform.localPosition = new Vector3(obj.transform.localPosition.x, diff.y, obj.transform.localPosition.z);
        inventory.Add(obj);
    }

    void remove(GameObject obj){
        obj.transform.parent = ground.transform;
        inventory.Remove(obj);
    }

    public void redoStack()
    {
        for(int i = 0; i < inventory.Count; i++)
        {
            GameObject obj = inventory[i];

            obj.transform.parent = transform;
            obj.transform.position = transform.position;
            obj.transform.localPosition = new Vector3(0, 0, 0);

            //Vector3 size = obj.GetComponent<Renderer>().bounds.size;
            Vector3 size = new Vector3(1f, 1f, 1f);
            Vector3 diff = (size * (i + 2));
            obj.transform.localPosition = new Vector3(obj.transform.localPosition.x, diff.y, obj.transform.localPosition.z);
        }
    }

    public void dropAll()
    {
        Vector3 currentVelocity = GetComponent<PlayerController>().getVelocity();
        for (int i = 0; i < inventory.Count; i++)
        {
            GameObject item = inventory[i];
            item.transform.SetParent(GameObject.Find("ItemDrops").transform);

            Rigidbody rigidBody = item.GetComponent<Rigidbody>();

            rigidBody.isKinematic       = false;
            rigidBody.detectCollisions  = true;
            rigidBody.useGravity        = true;

            rigidBody.velocity = new Vector3(Random.Range(-explosiveness, explosiveness), Random.Range(0, explosiveness), Random.Range(-explosiveness, explosiveness));

            item.GetComponent<Pickupable>().enabled = true;

        }
        inventory.Clear();
    }
}
