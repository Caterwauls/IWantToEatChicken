using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ENCollisionReporter : MonoBehaviour
{
    public bool didEnterTrigger = false;

    private void OnTriggerEnter(Collider other)
    {
        didEnterTrigger = true;
    }
}
