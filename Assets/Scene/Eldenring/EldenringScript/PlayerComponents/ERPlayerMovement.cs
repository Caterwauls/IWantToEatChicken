using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ERPlayerMovement : ERPlayerComponent
{
    public bool isSprinting { get; private set; }
    
    public Transform movementDirectionBase;
    public float maxSpeed = 10;
    public float sprintSpeed = 20;
    public float accelerationOnGround = 100;
    public float decelerationOnGround = 100;
    public float torqueMultiplier = 2f;

    public float spaceHoldThreshold = 0.25f;

    public float rollStartSpeed = 50f;
    public float rollEndSpeed = 10f;
    public float rollStartUpSpeed = 10f;
    public float rollEndUpSpeed = -10f;
    public float rollInvincibleDelay = 0.1f;
    public float rollInvincibleDuration = 0.4f;
    public float rollDuration = 0.7f;
    public float rollSpinSpeed = 8f;

    private ERPlayerGroundedChecker _grounded;
    private Vector3 _rollFallbackDir;

    protected override void Awake()
    {
        base.Awake();
        UseComponent(out _grounded);
    }

    private void Update()
    {
        if (_player.isStunned || _player.isChanneling || !_grounded.isGrounded) return;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(CheckSpaceActionRoutine());
        }
    }

    private IEnumerator CheckSpaceActionRoutine()
    {
        float startTime = Time.time;
        while (Time.time - startTime < spaceHoldThreshold)
        {
            // Cannot roll neither sprint; break!
            if (_player.isStunned || _player.isChanneling || !_grounded.isGrounded)
                yield break;
            
            if (Input.GetKeyUp(KeyCode.Space))
            {
                // Roll!    
                DoRoll();
            }
            yield return null;
        }
        
        // Sprint!
        isSprinting = true;
        while (true)
        {
            if (Input.GetKeyUp(KeyCode.Space) || _player.isStunned || _player.isChanneling || !_grounded.isGrounded)
            {
                isSprinting = false;
                yield break;
            }
            yield return null;
        }
    }

    private void DoRoll()
    {
        StartCoroutine(RollRoutine());
        IEnumerator RollRoutine()
        {
            var axisDir = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            Vector3 rollDir = _rollFallbackDir;
            if (axisDir.sqrMagnitude > 0.01f)
            {
                rollDir = Vector3.zero;
                rollDir += axisDir.x * movementDirectionBase.right;
                var flatForward = movementDirectionBase.forward;
                flatForward.y = 0;
                flatForward.Normalize();
                rollDir += axisDir.y * flatForward;
                rollDir = rollDir.normalized;
            }

            var startTime = Time.time;
            var didGrantInvincible = false;
            _player.StartChannel(rollDuration);
            _rb.rotation = Quaternion.LookRotation(rollDir);
            while (Time.time - startTime < rollDuration)
            {
                var v = (Time.time - startTime) / rollDuration;
                var rollSpeed = Mathf.Lerp(rollStartSpeed, rollEndSpeed, v);
                var upSpeed = Mathf.Lerp(rollStartUpSpeed, rollEndUpSpeed, v);
                // Apply vel
                _rb.velocity = rollDir * rollSpeed + Vector3.up * upSpeed;
                
                // Spin it
                var rightOfMovement = Vector3.Cross(Vector3.up, rollDir).normalized;

                transform.RotateAround(transform.position, rightOfMovement, rollSpinSpeed * Time.deltaTime);
                
                if (!didGrantInvincible && Time.time - startTime > rollInvincibleDelay)
                {
                    didGrantInvincible = true;
                    _player.GrantInvincible(rollInvincibleDuration);
                }
                yield return null;
            }
        }
    }

    private void FixedUpdate()
    {
        if (_grounded.isGrounded && !_player.isStunned && !_player.isChanneling)
        {
            var axisDir = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

            var rbVel = _rb.velocity;
            var rbFlatVel = new Vector2(rbVel.x, rbVel.z);
            
            // Not moving, decelerate!
            if (axisDir.sqrMagnitude < 0.1f)
            {
                if (rbVel.magnitude < 5f)
                    return; // No need for deceleration.
                rbFlatVel = rbFlatVel.normalized *
                          Mathf.MoveTowards(rbFlatVel.magnitude, 0, decelerationOnGround * Time.fixedDeltaTime);
                _rb.velocity = new Vector3(rbFlatVel.x, _rb.velocity.y, rbFlatVel.y);
                return;
            }
            
            // Apply velocity
            axisDir.Normalize();
            var desiredVel = Vector3.zero;
            desiredVel += axisDir.x * movementDirectionBase.right;
            var flatForward = movementDirectionBase.forward;
            flatForward.y = 0;
            flatForward.Normalize();
            desiredVel += axisDir.y * flatForward;
            desiredVel = desiredVel.normalized * (isSprinting ? sprintSpeed : maxSpeed);

            var desiredFlatVel = new Vector2(desiredVel.x, desiredVel.z);

            rbFlatVel = Vector2.MoveTowards(rbFlatVel, desiredFlatVel, accelerationOnGround * Time.fixedDeltaTime);
            
            var rbNewVel = rbVel;
            rbNewVel.x = rbFlatVel.x;
            rbNewVel.z = rbFlatVel.y;
            _rb.velocity = rbNewVel;
            
            // Get roll fallback velocity
            _rollFallbackDir = rbNewVel;
            _rollFallbackDir.y = 0;
            _rollFallbackDir.Normalize();
            
            // Apply angular velocity
            var rightOfMovement = Vector3.Cross(Vector3.up, desiredVel).normalized;
            _rb.AddTorque(rightOfMovement * (desiredVel.magnitude * torqueMultiplier * Time.fixedDeltaTime), ForceMode.VelocityChange);
        }
    }
}
