using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ERPlayerAdditionalGravity : ERPlayerComponent
{
    public float additionalGravity = 5;

    private void FixedUpdate()
    {
        _rb.velocity += Vector3.down * (Time.fixedDeltaTime * additionalGravity);
    }
}
