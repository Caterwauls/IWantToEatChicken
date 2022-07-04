using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ERPlayerAttack : ERPlayerComponent
{
    public Transform directionBase;
    
    public CapsuleCollider swordCollider;
    public ERDamageCollider swordDamageCollider;
    
    public Transform swordTransform;
    public Transform swordPivotTransform;

    public float swingReserveTime = 0.25f;
    
    public Vector3 swordPivotOffset;
    public float swordPivotRotSpeed = 360f;
    public float swordPivotSmoothTime = 0.2f;

    public Effect fxSwing;
    public float swordSwingDuration = 0.4f;
    public Vector3 swordIdleRot;
    public float swordIdleRotSpeed = 720f;
    public Vector3 swordSwingStartRot;
    public Vector3 swordSwingEndRot;
    public Vector3 playerSwingVelocity;
    public Vector3 playerSwingAngularVelocity;


    public float swordWieldDuration = 7f;
    public float swordWieldSpeed = 5f;

    public float swingStaminaCost = 20;
    public float swingChannelTime = 0.7f;

    public float blockedStunDuration = 1f;
    public float blockedStaminaCost = 10f;
    public Vector3 blockedAngularVel;
    public Vector3 blockedVel;

    private float _lastSwordUseTime = Mathf.NegativeInfinity;
    private float _currentSwingReserveTime = 0;

    private Vector3 _pivotCv;

    private void Start()
    {
        swordDamageCollider.onAttackBlocked += _ =>
        {
            _player.ApplyStun(blockedStunDuration);
            _player.UseStamina(blockedStaminaCost);
            _rb.velocity = directionBase.rotation * blockedVel;
            _rb.angularVelocity = directionBase.rotation * blockedAngularVel;
        };
    }

    public void HolsterSword()
    {
        _lastSwordUseTime = Mathf.NegativeInfinity;
    }

    private void Update()
    {
        // Keep Sword Pivot Position/Rotation
        if (!_player.isStunned)
        {
            var desiredPivotRot = Quaternion.Euler(0, directionBase.eulerAngles.y, 0);
            var desiredPivotPos = transform.position + desiredPivotRot * swordPivotOffset;
            swordPivotTransform.rotation = Quaternion.RotateTowards(swordPivotTransform.rotation, desiredPivotRot,
                swordPivotRotSpeed * Time.deltaTime);
            swordPivotTransform.position = Vector3.SmoothDamp(swordPivotTransform.position, desiredPivotPos, ref _pivotCv,
                swordPivotSmoothTime);
        }
        
        // Idle Sword Rotation
        if (!_player.isStunned && !_player.isChanneling)
        {
            swordTransform.localRotation =
                Quaternion.RotateTowards(swordTransform.localRotation, Quaternion.Euler(swordIdleRot), swordIdleRotSpeed * Time.deltaTime);
        }
        
        // Wield/Unwield Animation
        if (Time.time - _lastSwordUseTime < swordWieldDuration)
        {
            swordTransform.localScale =
                Vector3.MoveTowards(swordTransform.localScale, Vector3.one, swordWieldSpeed * Time.deltaTime);
        }
        else
        {
            swordTransform.localScale =
                Vector3.MoveTowards(swordTransform.localScale, Vector3.zero, swordWieldSpeed * Time.deltaTime);
        }
        
        // Get Input
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            _currentSwingReserveTime = swingReserveTime;
        }
        else
        {
            _currentSwingReserveTime = Mathf.MoveTowards(_currentSwingReserveTime, 0, Time.deltaTime);
        }
        
        if (_currentSwingReserveTime > 0 && _player.canChannel)
        {
            _currentSwingReserveTime = 0;
            SwingSword();
        }
    }

    private void SwingSword()
    {
        if (_player.stamina < swingStaminaCost) return;
        StartCoroutine(SwingSwordRoutine());
        IEnumerator SwingSwordRoutine()
        {
            fxSwing.Play();
            _player.StartChannel(swingChannelTime);
            _player.UseStamina(swingStaminaCost);
        
            _lastSwordUseTime = Time.time;
            swordCollider.enabled = true;
            var startRot = Quaternion.Euler(swordSwingStartRot);
            var endRot = Quaternion.Euler(swordSwingEndRot);
            var startTime = Time.time;
            var elapsedTime = 0f;

            var flatRot = Quaternion.Euler(0, directionBase.rotation.eulerAngles.y, 0);
            _rb.velocity = flatRot * playerSwingVelocity;
            _rb.angularVelocity = flatRot * playerSwingAngularVelocity;
            while (elapsedTime < swingChannelTime)
            {
                if (_player.isStunned)
                {
                    swordCollider.enabled = false;
                    yield break;
                }
                swordTransform.localRotation = Quaternion.Slerp(startRot, endRot, elapsedTime / swordSwingDuration);
                yield return null;
                elapsedTime = Time.time - startTime;
                if (elapsedTime > swordSwingDuration) swordCollider.enabled = false;
            }
            swordCollider.enabled = false;
        }
    }
}
