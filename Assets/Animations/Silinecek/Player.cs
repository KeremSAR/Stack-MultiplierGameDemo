using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float movementSpeed = 6;
    public float touchSensitivity = 0.6f;
    private Touch _touch;
    private Animator _rotationAnimator;
    private float _rotation;
    public float rotationSpeed;
    public float slowerResetInPercentage;
    private float touchMovementDeltaX;
    //Curve Way
    public float yRotation;
    public float curveRotationSpeed;
    //Sensors
    private RaycastHit _hitGround;
    private RaycastHit _hitLeft;
    private RaycastHit _hitRight;
    private bool _isGrounded;
    private bool _rightIsBlocked;
    private bool _leftIsBlocked;
    public LayerMask sensorLayerMask;
    public LayerMask groundSensorLayerMask;
    public float range;
    public float groundSensorRange;

    public Transform cameraTransform;
    public int slowerCameraThanPlayer;
   
    private static readonly int CharacterRotation = Animator.StringToHash("Rotation");
    
    void Start()
    {
        _rotationAnimator = GetComponent<Animator>();

        yRotation = transform.eulerAngles.y;
    }

    private void Movement()
    {
        if (Input.touchCount > 0)
        {
            _touch = Input.GetTouch(0);
        }

        if (Input.touchCount > 0)
        {
            _touch = Input.GetTouch(0); // Getting touch

            // Check if player is moving his/her finger
            if (_touch.phase == TouchPhase.Moved)
            {
                touchMovementDeltaX = _touch.deltaPosition.x;

                if (touchMovementDeltaX > 0 && _rightIsBlocked || touchMovementDeltaX < 0 && _leftIsBlocked)
                {
                    touchMovementDeltaX = 0;
                }

                transform.position += transform.right * touchSensitivity * touchMovementDeltaX * Time.deltaTime;

                LocalRotationManager(true);
            }
            else
            {
                LocalRotationManager(false);
            }

            
        }
        
        transform.position += Time.deltaTime * transform.forward * movementSpeed;
        
        CameraManager();
    }

    private void LocalRotationManager(bool moving)
    {
        if (moving)
        {
            if (touchMovementDeltaX > 1f)
            {
                _rotation -= Time.deltaTime * rotationSpeed;
            }
            else if (touchMovementDeltaX < -1f)
            {
                _rotation += Time.deltaTime * rotationSpeed;
            }
        }
        else
        {
            if (_rotation > 1)
            {
                _rotation -= Time.deltaTime * (rotationSpeed - ((rotationSpeed * slowerResetInPercentage) / 100));
            }
            else if (_rotation < -1)
            {
                _rotation += Time.deltaTime * (rotationSpeed - ((rotationSpeed * slowerResetInPercentage) / 100));
            }
            else
            {
                _rotation = 0;
            }
        }


        if (_rotation > 50)
        {
            _rotation = 50;
        }

        if (_rotation < -50)
        {
            _rotation = -50;
        }


        _rotationAnimator.SetFloat(CharacterRotation, -_rotation); // Applying rotation
    }

    private void SensorManager()
    {
        // Left sensors
        _isGrounded = Physics.Raycast(transform.position, -transform.up, out _hitGround, groundSensorRange, groundSensorLayerMask);

        

        // Left sensors
        _leftIsBlocked = Physics.Raycast(transform.position, -transform.right, out _hitLeft, range, sensorLayerMask);

        // Right sensors
        _rightIsBlocked = Physics.Raycast(transform.position, transform.right, out _hitRight, range, sensorLayerMask);
    }
    private void CurveManager()
    {
        if (!transform.eulerAngles.y.Equals(yRotation))
        {
            transform.eulerAngles = Vector3.MoveTowards(transform.eulerAngles, new Vector3(0, yRotation, 0), Time.deltaTime * curveRotationSpeed);
        }
    }

    private void CameraManager()
    {
        cameraTransform.position = transform.position;
        cameraTransform.eulerAngles = Vector3.MoveTowards(cameraTransform.eulerAngles, transform.eulerAngles, Time.deltaTime * (curveRotationSpeed - ((curveRotationSpeed * slowerCameraThanPlayer) / 100)));
    }

    private void FixedUpdate()
    {
        SensorManager();
        Movement();
        CurveManager();
    }

    
}
