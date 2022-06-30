using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ERPlayerGroundedChecker : ERPlayerComponent
{
    public bool isGrounded { get; private set; }
    
    public float raycastDistance = 1.25f;
    
    private ERPlayerJump _jump;
    
    protected override void Awake()
    {
        base.Awake();
        UseComponent(out _jump);
    }

    private void Update()
    {
        UpdateIsGrounded();
    }

    private void UpdateIsGrounded()
    {
        if (Time.time - _jump.lastJumpTime < 0.25f)
        {
            isGrounded = false;
            return;
        }

        isGrounded = Physics.Raycast(transform.position, Vector3.down, raycastDistance, LayerMask.GetMask("Ground"));
    }
}
