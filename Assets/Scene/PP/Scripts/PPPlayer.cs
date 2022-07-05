using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PPPlayer : MonoBehaviour
{
    public bool canPlayerMove = false;

    public bool canResist = false;
    public int restraintCount = 0;

    public float playerAcceleration = 50f;
    public float ballSpeed = 10;

    public GameObject resistEffect;
    public GameObject breakFreeEffect;
    public GameObject bounceEffect;

    public int hDir = 1;
    public int vDir = 1;

    private float _lastHChange = float.NegativeInfinity;
    private float _lastVChange = float.NegativeInfinity;

    private Rigidbody _rb;
    
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (restraintCount > 0)
        {
            _rb.velocity = new Vector3(ballSpeed * hDir, ballSpeed * vDir, 0);
            _rb.angularVelocity = Vector3.zero;
            _rb.rotation = Quaternion.identity;
            return;
        }
        if (!canPlayerMove) return;
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");

        Vector3 vec = new Vector3(inputX, inputY, 0);
        if (vec.magnitude < 0.01f) return;
        vec.Normalize();
        GetComponent<Rigidbody>().velocity += vec * Time.fixedDeltaTime * playerAcceleration;
    }

    private void Update()
    {
        if (canResist && restraintCount > 0)
        {
            DoResistCheck();
        }
    }

    private void DoResistCheck()
    {
        if (!Input.GetKeyDown(KeyCode.Space)) return;
        restraintCount--;
        Instantiate(
            restraintCount == 0 ? breakFreeEffect : resistEffect, 
            transform.position, 
            Quaternion.identity);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (restraintCount == 0) return;
        var scale = collision.collider.transform.localScale;
        if (scale.x < scale.y && Time.time - _lastHChange > 0.5f)
        {
            _lastHChange = Time.time;
            hDir *= -1;
            Instantiate(bounceEffect, transform.position, Quaternion.identity);
        }
        else if (scale.x > scale.y && Time.time - _lastVChange > 0.5f)
        {
            _lastVChange = Time.time;
            vDir *= -1;
            Instantiate(bounceEffect, transform.position, Quaternion.identity);
        }


    }
}
