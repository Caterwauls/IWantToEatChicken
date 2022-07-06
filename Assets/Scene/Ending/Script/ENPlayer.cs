using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ENPlayer : MonoBehaviour
{
    public Transform movementBase;
    public bool canMove;
    public float moveSpeed = 10f;
    public float acceleration = 30f;

    private Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        var inputAxis = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (!canMove) return;
        if (inputAxis.magnitude < 0.1f) return;
        inputAxis.Normalize();
        var vel = movementBase.rotation * new Vector3(inputAxis.x, 0, inputAxis.y);
        vel.y = 0f;
        vel.Normalize();
        vel *= moveSpeed;
        _rb.velocity = Vector3.MoveTowards(_rb.velocity, vel, acceleration * Time.deltaTime);
        var rightOfMovement = Vector3.Cross(Vector3.up, vel).normalized;
        _rb.AddTorque(rightOfMovement * (vel.magnitude * 2 * Time.fixedDeltaTime), ForceMode.VelocityChange);
    }
}
