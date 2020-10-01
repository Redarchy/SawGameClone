using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cleaner : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "RightHull" || other.gameObject.tag == "LeftHull")
        {
            Destroy(other.gameObject);
            
        }
        else if(other.gameObject.tag == "Log")
        {
            other.gameObject.SetActive(false);
            SpawnManager.instance.Enqueue(other.gameObject);
            EventController.instance.OnScoreUpdate(-20);
        }
    }
}
