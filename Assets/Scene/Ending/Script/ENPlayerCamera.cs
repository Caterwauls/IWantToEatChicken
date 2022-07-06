using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ENPlayerCamera : MonoBehaviour
{
    public Transform target;
    public float posSmoothTime = 1f;
    public float rotSpeed = 600;
    
    private Vector3 _offset;
    private Vector3 _cv;

    private void Awake()
    {
        _offset = transform.position - target.position;
        UpdateImmediately();
    }

    private void Update()
    {
        transform.position = Vector3.SmoothDamp(transform.position, target.position + _offset, ref _cv, posSmoothTime);
        transform.rotation = Quaternion.RotateTowards(transform.rotation,
            Quaternion.LookRotation(target.position - transform.position), Time.deltaTime * rotSpeed);
    }

    public void UpdateImmediately()
    {
        transform.position = target.position + _offset;
        transform.rotation = Quaternion.LookRotation(target.position - transform.position);
    }
}
