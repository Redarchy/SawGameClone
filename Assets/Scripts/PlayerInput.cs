using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerInput : MonoBehaviour, IPointerDownHandler
{
    public float speed;

    public GameObject saw;
    public Vector3 desiredPos;

    private void Start()
    {
        desiredPos = saw.transform.position;
    }
    private void Update()
    {
        saw.transform.position = Vector3.Lerp(saw.transform.position, desiredPos, Time.deltaTime * speed);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        var ray = Camera.main.ScreenPointToRay (Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            desiredPos = new Vector3(saw.transform.position.x, saw.transform.position.y, hit.point.z);
        }
    }
}
