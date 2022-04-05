﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool jumpCheck = true;

    private Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();

    }

    private void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Space)) && (jumpCheck))
        {
            jumpCheck = false;
            CubeJump();
            StartCoroutine(JumpDelay());
        }

    }

    void FixedUpdate()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputZ = Input.GetAxis("Vertical");

        Vector3 velocity = new Vector3(inputX, 0, inputZ);

        velocity *= 10f;

        MoveMyVelocity(velocity);

    }

    public void MoveMyVelocity(Vector3 targetVelocity)
    {

        Vector3 newVelocity = Vector3.MoveTowards(_rb.velocity, targetVelocity, 20f * Time.fixedDeltaTime);
        newVelocity.y = _rb.velocity.y;
        _rb.velocity = newVelocity;
    }

    public void CubeJump()
    {
        _rb.AddForce(Vector3.up.normalized * 10f, ForceMode.Impulse);
    }

    IEnumerator JumpDelay()
    {

        yield return new WaitForSeconds(2);
        jumpCheck = true;

    }
}
