using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;

public class BBPlayerAbility : BBPlayerComponentBase
{
    private static Collider[] _colBuffer = new Collider[128];
    
    public float dashAbilityDis = 10f;
    public bool dashAbilityOn = false;
    public float dashSpeed = 40f;
    public int dashSweepCount = 15;
    public GameObject dashAbilityEffect;

    public bool flyAbilityOn = false;
    public GameObject flyAbility;
    public GameObject flyAbilityStartEffect;

    public bool doubleJumpOn = false;
    public GameObject doubleJumpEffect;

    public GameObject timeStopVolume;
    public GameObject timeStopWave;

    private void Update()
    {
        if (dashAbilityOn)
        {
            CheckDashAbility();
        }
        else if (flyAbilityOn)
        {
            CheckFlyAbility();
        }
        else if (doubleJumpOn)
        {
            CheckDoubleJump();
        }
    }

    void CheckDashAbility()
    {
        if (!Input.GetKeyDown(KeyCode.F)) return;
        
        Instantiate(dashAbilityEffect, transform.position, Quaternion.identity);
        dashAbilityOn = false;

        var hAxis = Input.GetAxis("Horizontal");
        Vector3 sweepDir = hAxis < 0 ? Vector3.left : Vector3.right;
        if (Mathf.Abs(hAxis) < 0.1f && Mathf.Abs(_rb.velocity.x) > 1f)
        {
            sweepDir = _rb.velocity.x < 0 ? Vector3.left : Vector3.right;
        }
        Vector3 start = transform.position;
        Vector3 destination = transform.position;

        for (int i = 0; i < dashSweepCount; i++)
        {
            var candidate = transform.position + dashAbilityDis * i / (dashSweepCount - 1f) * sweepDir;
            if (Physics.OverlapBoxNonAlloc(candidate, Vector3.one * 0.4f, _colBuffer, Quaternion.identity) > 0) 
                continue;
            destination = candidate;
        }

        var dashDuration = (destination - start).magnitude / dashSpeed;
        StartCoroutine(InterpolatePosition());
        IEnumerator InterpolatePosition()
        {
            _rb.isKinematic = true;
            for (float t = 0; t < dashDuration; t += Time.deltaTime)
            {
                transform.position = Vector3.Lerp(start, destination, t / dashDuration);
                yield return null;
            }
            _rb.velocity = Vector3.zero;
            _rb.isKinematic = false;
        }
    }

    void CheckFlyAbility()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            flyAbilityOn = false;
            GetComponent<MeshRenderer>().enabled = false;
            _player.movement.playerMoveOn = false;
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            GetComponent<Rigidbody>().isKinematic = true;
            Instantiate(flyAbilityStartEffect, transform.position, Quaternion.identity);
            Instantiate(flyAbility, transform.position, Quaternion.identity);
        }

    }

    void CheckDoubleJump()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            doubleJumpOn = false;
            var bottomPos = transform.position + Vector3.up * transform.lossyScale.y / 2f;
            Instantiate(doubleJumpEffect, bottomPos, Quaternion.identity);
            Vector3 newVelocity = _rb.velocity;
            newVelocity.y = GetComponent<BBPlayerMovement>().jumpSpeed * 1.5f;
            _rb.velocity = newVelocity;

            if (GetComponent<BBPlayerReverseTile>().reverseOn)
            {
                newVelocity.y = -GetComponent<BBPlayerMovement>().jumpSpeed * 1.5f;
                _rb.velocity = newVelocity;
            }
        }
    }
}
