using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class Pool : MonoBehaviour
{
    private Queue<GameObject> pool;
    private Material[] materials;

    private void Awake()
    {
        CreateMaterials();
    }

    public void CreatePool(int logPool)
    {
        
        pool = new Queue<GameObject>();
        for (int i = 0; i < logPool; i++)
        {
            GameObject obj = Instantiate(Resources.Load("Prefabs/Log") as GameObject);
            
            obj.tag = "Log";
            obj.GetComponent<MeshRenderer>().material = GetRandomMaterial();
            obj.transform.localScale = new Vector3(obj.transform.localScale.x,obj.transform.localScale.y  * GenerateRandomFloat(), obj.transform.localScale.z);
            obj.SetActive(false);
            
            Enqueue(obj);
        }
    }

    private void CreateMaterials()
    {
        materials = new[]
        {
            Resources.Load("Materials/Colors/Blue") as Material,
            Resources.Load("Materials/Colors/Brown") as Material,
            Resources.Load("Materials/Colors/Gray") as Material,
            Resources.Load("Materials/Colors/Lilac") as Material,
            Resources.Load("Materials/Colors/White") as Material,
            Resources.Load("Materials/Colors/Yellow") as Material
        };
    }

    private float GenerateRandomFloat()
    {
        return UnityEngine.Random.Range(0.8f, 1.5f);
    }

    private Material GetRandomMaterial()
    {
        return materials[UnityEngine.Random.Range(0, materials.Length)];
    }
    
    public void Enqueue(GameObject obj)
    {
        pool.Enqueue(obj);
    }

    public GameObject Peek()
    {
        return pool.Peek();
    }
    public GameObject Dequeue()
    {
        return pool.Dequeue();
    }
}
