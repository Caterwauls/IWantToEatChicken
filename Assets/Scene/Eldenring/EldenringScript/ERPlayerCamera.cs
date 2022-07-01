using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class ERPlayerCamera : MonoBehaviour
{
    public ERPlayer player;
    public Vector3 pivotWorldOffset = Vector3.up;
    public float camSensitivity = 3;
    public float camDistance = 7;
    public float camDistanceSprinting = 10;
    public float sphereCastRadius = 1f;
    public float yCoordinateSmoothTime = 0.2f;

    public float pitch = 0;
    public float yaw = 0;

    private float _currentCamDistance;
    private float _currentCamDistanceDampVel;

    private float _camYCurrentVel;
    
    private CinemachineVirtualCamera _vCam;
    private ERPlayerMovement _movement;
    
    private void Awake()
    {
        _vCam = GetComponent<CinemachineVirtualCamera>();
        _movement = player.GetComponent<ERPlayerMovement>();
        _currentCamDistance = camDistance;
    }

    private void LateUpdate()
    {
        float desiredCamDistance = _movement.isSprinting ? camDistanceSprinting : camDistance;
        _currentCamDistance = Mathf.SmoothDamp(_currentCamDistance, desiredCamDistance, ref _currentCamDistanceDampVel, 0.2f);
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
        var mouseX = Input.GetAxis("Mouse X");
        var mouseY = Input.GetAxis("Mouse Y");

        pitch = Mathf.Clamp(pitch - mouseY * camSensitivity, -89, 89);
        yaw = Mathf.Repeat(yaw + mouseX * camSensitivity, 360);

        var camRot = Quaternion.Euler(0, yaw, 0) * Quaternion.Euler(pitch, 0, 0);
        transform.rotation = camRot;

        var pivot = player.transform.position + pivotWorldOffset;
        Vector3 desiredPos;
        
        if (Physics.SphereCast(pivot, sphereCastRadius, camRot * Vector3.back, out RaycastHit hit,
                _currentCamDistance, LayerMask.GetMask("Ground")))
        {
            desiredPos = pivot + transform.rotation * Vector3.back * hit.distance;
        }
        else
        {
            desiredPos = pivot + transform.rotation * Vector3.back * _currentCamDistance;
        }

        var newPos = desiredPos;
        newPos.y = Mathf.SmoothDamp(transform.position.y, desiredPos.y, ref _camYCurrentVel, yCoordinateSmoothTime);
        transform.position = newPos;
    }
}
