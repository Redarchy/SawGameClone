using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutArea : MonoBehaviour
{
    public bool CutRight = false;

    private void OnEnable()
    {
        CutRight = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Saw")
        {
            CutRight = true;
            Debug.Log("temas var");
        }
    }
}
