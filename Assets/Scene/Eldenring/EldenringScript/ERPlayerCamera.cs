using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class ERPlayerCamera : MonoBehaviour
{
    public static Collider[] _collisionCheckBuffer = new Collider[512];
    
    public ERPlayer player;
    public Vector3 pivotWorldOffset = Vector3.up;
    
    public EREnemy lockOnTarget;
    public float lockOnMaxDistance = 40f;
    
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

    private float _lockOnPitchCv;
    private float _lockOnYawCv;

    public float lockOnSwitchCooldown = 0.5f;
    public float lockOnSwitchThreshold = 30f;
    public float lockOnSwitchBiasMultiplier = 2f;
    public Vector2 lockedOnAngleOffset;
    private float _lastLockOnSwitchTime = float.NegativeInfinity;

    private CinemachineVirtualCamera _vCam;
    private ERPlayerMovement _movement;
    
    private void Awake()
    {
        _vCam = GetComponent<CinemachineVirtualCamera>();
        _movement = player.GetComponent<ERPlayerMovement>();
        _currentCamDistance = camDistance;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (lockOnTarget == null) FindLockOnTarget(enemy => -Vector3.Angle(transform.forward, enemy.transform.position - transform.position));
            else lockOnTarget = null;
        }
            
        
        if (lockOnTarget != null && (lockOnTarget.isDead || Vector3.Distance(player.transform.position, lockOnTarget.transform.position) > lockOnMaxDistance))
        {
            lockOnTarget = null;
            // Find Enemy with Smallest Angle
            FindLockOnTarget(enemy => -Vector3.Angle(transform.forward, enemy.transform.position - transform.position));
        }
    }

    private void LateUpdate()
    {
        float desiredCamDistance = _movement.isSprinting ? camDistanceSprinting : camDistance;
        _currentCamDistance = Mathf.SmoothDamp(_currentCamDistance, desiredCamDistance, ref _currentCamDistanceDampVel, 0.2f);
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        var mouseX = Input.GetAxis("Mouse X");
        var mouseY = Input.GetAxis("Mouse Y");
        
        if (lockOnTarget != null)
        {
            var desiredRot = Quaternion.LookRotation(lockOnTarget.lockOnPosition.position - transform.position);
            var euler = desiredRot.eulerAngles;
            
            pitch = Mathf.SmoothDampAngle(pitch, euler.x + lockedOnAngleOffset.y, ref _lockOnPitchCv, 0.1f);
            yaw = Mathf.SmoothDampAngle(yaw, euler.y + lockedOnAngleOffset.x, ref _lockOnYawCv, 0.1f);

            var curVec = new Vector2(mouseX, mouseY) * camSensitivity;
            if (Time.time - _lastLockOnSwitchTime > lockOnSwitchCooldown && curVec.magnitude > lockOnSwitchThreshold)
            {
                _lastLockOnSwitchTime = Time.time;
                var testPitch = Mathf.Clamp(pitch - mouseY * camSensitivity * lockOnSwitchBiasMultiplier, -89, 89);
                var testYaw = Mathf.Repeat(yaw + mouseX * camSensitivity * lockOnSwitchBiasMultiplier, 360);
                var testForward = Quaternion.Euler(0, testYaw, 0) * Quaternion.Euler(testPitch, 0, 0) * Vector3.forward;
                FindLockOnTarget(enemy => -Vector3.Angle(testForward, enemy.transform.position - transform.position));
            }
        }
        else
        {
            pitch = Mathf.Clamp(pitch - mouseY * camSensitivity, -89, 89);
            yaw = Mathf.Repeat(yaw + mouseX * camSensitivity, 360);
        }
        
        var camRot = Quaternion.Euler(0, yaw, 0) * Quaternion.Euler(pitch, 0, 0);
        transform.rotation = camRot;
        
        var pivot = player.transform.position + pivotWorldOffset;
        Vector3 desiredPos;
        
        if (Physics.SphereCast(pivot, sphereCastRadius, transform.rotation * Vector3.back, out RaycastHit hit,
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

    private void FindLockOnTarget(Func<EREnemy, float> scoreFunction = null)
    {
        var num = Physics.OverlapSphereNonAlloc(player.transform.position, lockOnMaxDistance, _collisionCheckBuffer, ~0, QueryTriggerInteraction.Collide);
        Dictionary<EREnemy, float> enemies = new Dictionary<EREnemy, float>();
        for (int i = 0; i < num; i++)
        {
            var col = _collisionCheckBuffer[i];
            var e = col.GetComponent<EREnemy>();
            if (e == null || e == lockOnTarget || e.isDead) continue;
            if (enemies.ContainsKey(e)) continue;
            if (scoreFunction == null)
            {
                lockOnTarget = e;
                return;
            }

            enemies.Add(e, scoreFunction(e));
        }

        if (enemies.Count == 0)
        {
            return;
        }

        float bestScore = float.NegativeInfinity;
        EREnemy bestEnemy = null;
        foreach (var pair in enemies)
        {
            if (bestScore > pair.Value) continue;
            bestScore = pair.Value;
            bestEnemy = pair.Key;
        }

        lockOnTarget = bestEnemy;
    }
}
