using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRandom : MonoBehaviour
{

 //public Transform plane;
 public GameObject[] spawnablePrefabs;
 //public static GameObject[] terrainPrefabs;

 
 public int NumberToSpawn;
 public float ScaleMin;
 public float ScaleMax;
 // Plane Properties
 float x_dim;
 float z_dim;
 int NumberSpawned = 0;

 void Start () {
    //terrainPrefabs = Resources.LoadAll<GameObject>("Terrain");
    Mesh _mesh = transform.GetComponent<MeshFilter> ().mesh;
    x_dim = _mesh.bounds.size.x;
    z_dim = _mesh.bounds.size.z;
 }

 void Update(){
         if (NumberSpawned < NumberToSpawn){
             SpawnInside(spawnablePrefabs[Random.Range(0, spawnablePrefabs.Length - 1)]);
             NumberSpawned++;
         }
     }

 public void SpawnInside(GameObject spawnObject){
         Vector3 randpos = Vector3.zero;
         Quaternion rot = Quaternion.Euler(0, Random.Range(0, 360), 0);
         randpos.x = Random.Range(-x_dim/2f, x_dim/2f);//assume mesh of the plane is centered, view mesh.bounds.min.x and mesh.bounds.max.x if not centered
         randpos.y = 0f;//"level" hoy much up to the plane spawn the objects
         randpos.z = Random.Range(-z_dim/2f, z_dim/2f);
         Transform instance = Instantiate (spawnObject, this.transform).transform;
         instance.localPosition = randpos;
         instance.rotation = rot;
         float scaleFactor = Random.Range(ScaleMin, ScaleMax);
         instance.localScale *= scaleFactor;
     }
}
