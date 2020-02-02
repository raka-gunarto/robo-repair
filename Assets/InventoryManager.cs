using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{

    public float explosiveness = 50;

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

    public void dropAll()
    {
        Vector3 currentVelocity = GetComponent<PlayerController>().getVelocity();
        for (int i = 0; i < inventory.Count; i++)
        {
            GameObject item = inventory[i];
            item.transform.SetParent(GameObject.Find("ItemDrops").transform);
            Rigidbody rigidBody = item.AddComponent<Rigidbody>();
            rigidBody.mass = 0.1f;
            rigidBody.velocity = currentVelocity;

            rigidBody.velocity.Set( 
                currentVelocity.x * Random.Range(0.3f, 15f) + Random.Range(-explosiveness, explosiveness),
                currentVelocity.y * Random.Range(0.3f, 15f) + Random.Range(-explosiveness, explosiveness),
                currentVelocity.z * Random.Range(0.3f, 15f) + Random.Range(-explosiveness, explosiveness)
            );

            MeshCollider collider = item.AddComponent<MeshCollider>();
            collider.convex = true;
        }
        inventory.RemoveRange(0, inventory.Count);
    }
}
