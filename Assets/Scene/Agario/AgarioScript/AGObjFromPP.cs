using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AGObjFromPP : MonoBehaviour
{
    public GameObject effect;
    public Vector3 angular;
    private float _lastHitTime = float.NegativeInfinity;

    private void Awake()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.angularVelocity = angular;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (Time.unscaledTime - _lastHitTime < 10000f) return;
        _lastHitTime = Time.unscaledTime;
        Instantiate(effect, transform.position, Quaternion.identity);
    }
}
