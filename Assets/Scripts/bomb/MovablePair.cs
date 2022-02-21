using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovablePair : MonoBehaviour
{
    private Camera _mainCamera;
    private float _cameraZDistance;
    private Vector3 _initialPosition;
    private bool _connected;
    
    private const string _portTag = "Port";
    private const float _dragResponseThreshold = 2;

    

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public void SetInitialPosition(Vector3 newPosition)
    {
        _initialPosition = newPosition;
        transform.position = _initialPosition;
    }

    


    void Start()
    {
        _mainCamera = Camera.main;
        _cameraZDistance = _mainCamera.WorldToScreenPoint(transform.position).z;
    }

    private void OnMouseDrag()
    {
        Vector3 ScreenPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, _cameraZDistance);
        Vector3 NewWorldPosition = _mainCamera.ScreenToWorldPoint(ScreenPosition);

        if (!_connected)
        {
            transform.position = NewWorldPosition;
        }
        else if (Vector3.Distance(transform.position, NewWorldPosition) > _dragResponseThreshold)
        {
            _connected = false;
        }
    }

    private void OnMouseUp()
    {
        if (!_connected)
        {
            ResetPosition();
        }
    }

    private void ResetPosition()
    {
        transform.position = _initialPosition;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(_portTag))
        {
            _connected = true;
            transform.position = other.transform.position;
        }
    }


    void Update()
    {
        
    }
}
