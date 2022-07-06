using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class PPOtherBall : MonoBehaviour
{
    public float ballSpeed = 10;

    public int hDir = 1;
    public int vDir = 1;

    private float _lastHChange = float.NegativeInfinity;
    private float _lastVChange = float.NegativeInfinity;

    private Rigidbody _rb;
    
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        hDir = UnityEngine.Random.value > 0.5f ? 1 : -1;
        vDir = UnityEngine.Random.value > 0.5f ? 1 : -1;
    }

    private void FixedUpdate()
    {
        _rb.velocity = new Vector3(ballSpeed * hDir, ballSpeed * vDir, 0);
        _rb.angularVelocity = Vector3.zero;
        _rb.rotation = Quaternion.identity;
    }

    private void OnCollisionEnter(Collision collision)
    {
        var scale = collision.collider.transform.localScale;
        if (scale.x < scale.y && Time.time - _lastHChange > 0.5f)
        {
            _lastHChange = Time.time;
            hDir *= -1;
        }
        else if (Time.time - _lastVChange > 0.5f)
        {
            _lastVChange = Time.time;
            vDir *= -1;
        }
    }
}
