using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BBPlayerAdditionalGravity : BBPlayerComponentBase
{
    public float additionalGravity = -10f;

    private BBPlayerReverseTile _reverse;
    protected override void Awake()
    {
        base.Awake();
        _reverse = GetComponent<BBPlayerReverseTile>();
    }

    private void FixedUpdate()
    {
        if (!_reverse.reverseOn)//노말상태
        {
            _rb.velocity += Vector3.down * additionalGravity * Time.fixedDeltaTime;
        }

        else if (_reverse.reverseOn)//반전상태
        {
            _rb.velocity += Vector3.up * additionalGravity * Time.fixedDeltaTime;
        }
    }
}
