using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ERPlayerJump : ERPlayerComponent
{
    public float lastJumpTime { get; private set; }

    public float jumpVelocity = 30;
    public GameObject jumpEffectPrefab;

    private float _currentJumpReserveTime = 0;
    private ERPlayerGroundedChecker _grounded;

    protected override void Awake()
    {
        base.Awake();
        UseComponent(out _grounded);
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
            _currentJumpReserveTime = 0.25f;
        else
            _currentJumpReserveTime = Mathf.MoveTowards(_currentJumpReserveTime, 0, Time.deltaTime);
    }

    private void FixedUpdate()
    {
        if (_player.canChannel && _grounded.isGrounded && _currentJumpReserveTime > 0)
        {
            _currentJumpReserveTime = 0;
            Instantiate(jumpEffectPrefab, transform.position, Quaternion.identity);
            var newJumpVel = _rb.velocity;
            newJumpVel *= 0.75f;
            newJumpVel.y = jumpVelocity;
            _rb.velocity = newJumpVel;
            lastJumpTime = Time.time;
            _player.StartChannel(0.25f);
        }
    }
}
