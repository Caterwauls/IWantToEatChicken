using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BBPlayerBulletTile : BBPlayerComponentBase
{
    private BBPlayerMovement _movement;
    private BBPlayerAdditionalGravity _gravity;
    public GameObject effect;

    protected override void Awake()
    {
        base.Awake();
        _movement = GetComponent<BBPlayerMovement>();
        _gravity = GetComponent<BBPlayerAdditionalGravity>();
    }

    private void Start()
    {
        GetComponent<BBPlayerMovement>().onBlockCollision += OnCollision;
    }

    private void OnCollision(RaycastHit hit)
    {
        if (hit.collider.tag != "BulletTile") return;
        StartCoroutine(BulletRoutine(hit));
    }

    IEnumerator BulletRoutine(RaycastHit hit)
    {
        Instantiate(effect, transform.position, Quaternion.identity);
        bool isRight = hit.transform.GetComponent<BBBulletTile>().tileDirRight;
        
        Vector3 wantedVel;
        if (isRight)
        {
            transform.position = hit.transform.position + Vector3.right * hit.transform.lossyScale.x;
            wantedVel = Vector3.right * _movement.ballSpeed * 2;
        }
        else
        {
            transform.position = hit.transform.position + Vector3.left * hit.transform.lossyScale.x;
            wantedVel = Vector3.left * _movement.ballSpeed * 2;
        }

        var startVel = Mathf.Abs(_rb.velocity.x);
        _gravity.enabled = false;
        _movement.enabled = false;
        hit.collider.enabled = false;

        float startTime = Time.time;

        while (true)
        {
            if (Time.time - startTime < 0.5f)
                _rb.velocity = wantedVel;

            // 부딪힐 경우
            if (Mathf.Abs(_rb.velocity.x) < startVel * 0.8f)
            {
                break;
            }

            // 사용자 입력이 올 경우
            if (Time.time - startTime > 0.5f && Mathf.Abs(Input.GetAxis("Horizontal")) > 0.1)
            {
                break;
            }
        
            yield return null;
        }
        var newVel = _rb.velocity;
        newVel.x = 0;
        _rb.velocity = newVel;
        _gravity.enabled = true;
        _movement.enabled = true;
        hit.collider.enabled = true;
    }
}
