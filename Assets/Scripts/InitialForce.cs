using System;
using UnityEngine;

public class InitialForce : MonoBehaviour
{
    private Rigidbody rb;
    
    public Vector3 initialPushForce = new Vector3(-600,0,0);
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    
    private void OnEnable()
    {
        Push(initialPushForce);
    }

    private void Push(Vector3 force)
    {
        rb.AddForce(force);
    }
}
