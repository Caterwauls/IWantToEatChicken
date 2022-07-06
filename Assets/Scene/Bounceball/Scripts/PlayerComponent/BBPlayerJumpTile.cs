using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BBPlayerJumpTile : BBPlayerComponentBase
{
    public GameObject jumpTileEffectPrefab;

    private BBPlayerReverseTile _reverse;
    private BBPlayerMovement _movement;
    
    protected override void Awake()
    {
        base.Awake();
        _reverse = GetComponent<BBPlayerReverseTile>();
        _movement = GetComponent<BBPlayerMovement>();
    }

    private void Start()
    {
        GetComponent<BBPlayerMovement>().onBlockCollision += OnCollision;
    }

    private void OnCollision(RaycastHit hit)
    {
        if (hit.collider.tag != "JumpTile") return;

        Instantiate(jumpTileEffectPrefab, hit.transform.position, Quaternion.identity);
        Vector3 jumpTileVelocity = _rb.velocity;
        jumpTileVelocity.y = _movement.jumpSpeed * 2;
        if (_reverse.reverseOn)
        {
            jumpTileVelocity.y = -_movement.jumpSpeed * 2;
        }

        jumpTileVelocity.x = 0;
        _rb.velocity = jumpTileVelocity;
        _movement.skipNormalJump = true;
    }
}
