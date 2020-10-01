using System;
using EzySlice;
using UnityEngine;

public class LogCuttable : MonoBehaviour, ICuttable
{
    private MeshFilter initialMeshFilter;
    private Material initialMaterial;

    private void Awake()
    {
        SaveInitialMesh();
        
    }

    public void SaveInitialMesh()
    {
        initialMeshFilter = GetComponent<MeshFilter>();
        initialMaterial = GetComponent<MeshRenderer>().material;
    }

    public void Cut(Vector3 planeWorldPosition, Vector3 planeWorldDirection)
    {
        SlicedHull cut = GetHull(planeWorldPosition, planeWorldDirection, new TextureRegion(0.0f, 0.0f, 1.0f, 1.0f));
        
        GameObject leftCut = cut.CreateUpperHull(gameObject);
        GameObject rightCut = cut.CreateLowerHull(gameObject);

        leftCut.tag = "LeftHull";
        rightCut.tag = "RightHull";
        
        AddComponents(leftCut, planeWorldDirection);
        AddComponents(rightCut, -planeWorldDirection);

        gameObject.SetActive(false);
        SpawnManager.instance.Enqueue(gameObject);
    }

    public SlicedHull GetHull(Vector3 planeWorldPosition, Vector3 planeWorldDirection, TextureRegion textureRegion)
    {
        return gameObject.Slice(planeWorldPosition, planeWorldDirection, textureRegion, initialMaterial);
    }

    public void AddComponents(GameObject obj, Vector3 planeWorldDirection)
    {
        obj.AddComponent<MeshCollider>().convex = true;
        obj.AddComponent<Rigidbody>();
        obj.GetComponent<Rigidbody>().interpolation = RigidbodyInterpolation.Interpolate;
        obj.GetComponent<Rigidbody>().AddForce(-130,0,planeWorldDirection.z * 100);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Saw")
        {
            Cut(other.transform.position, other.transform.up);
            
            if (GetComponentInChildren<CutArea>().CutRight)
            {
                EventController.instance.OnScoreUpdate(10);
            }
            else
            {
                EventController.instance.OnScoreUpdate(-5);
            }
            
            Debug.Log(ScoreManager.instance.GetCurrentScore());
        }
        
        
    }
}
