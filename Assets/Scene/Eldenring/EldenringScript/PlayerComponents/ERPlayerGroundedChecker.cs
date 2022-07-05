using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ERPlayerGroundedChecker : ERPlayerComponent
{
    public bool isGrounded { get; private set; }
    
    public float raycastDistance = 1.25f;
    public GameObject landEffectPrefab;
    
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

        if (!isGrounded &&
            Physics.Raycast(transform.position, Vector3.down, raycastDistance, LayerMask.GetMask("Ground")))
        {
            isGrounded = true;
            Instantiate(landEffectPrefab, transform.position, Quaternion.identity);
        }
    }
}
