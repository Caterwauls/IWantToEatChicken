using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PPBreakable : MonoBehaviour
{
    public int maxHP;
    public int curHP;

    public float breakAngularMin;
    public float breakAngularMax;
    public float breakLinearMin;
    public float breakLinearMax;
    public float returnDuration = 1.5f;

    public GameObject damageEffect;
    public GameObject breakEffect;
    public GameObject returnEffect;
    
    private Vector3 _originalPos;
    private Quaternion _originalRot;

    private Rigidbody _rb;
    private float _lastHitTime;
    
    private void Start()
    {
        curHP = maxHP;
        _originalPos = transform.position;
        _originalRot = transform.rotation;
        _rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        var player = collision.collider.GetComponent<PPPlayer>();
        if (player == null || player.restraintCount > 0) return;
        if (Time.time - _lastHitTime < 0.5f) return;
        _lastHitTime = Time.time;
        var otherRb = collision.collider.GetComponent<Rigidbody>();
        otherRb.velocity *= -3f;
        otherRb.velocity = otherRb.velocity.normalized * 8f;
        curHP--;
        if (curHP == 0)
        {
            Instantiate(breakEffect, transform.position, Quaternion.identity);
            _rb.isKinematic = false;
            _rb.velocity = Random.onUnitSphere * Random.Range(breakLinearMin, breakLinearMax);
            _rb.angularVelocity = new Vector3(0, 0, Random.Range(breakAngularMin, breakAngularMax));
        }
        else
        {
            Instantiate(damageEffect, transform.position, Quaternion.identity);
        }
    }

    public void ReturnToOriginal()
    {
        StartCoroutine(Routine());
        IEnumerator Routine()
        {
            curHP = maxHP;
            _rb.isKinematic = true;
            
            Vector3 startPos = transform.position;
            Quaternion startRot = transform.rotation;
            
            for (float t = 0; t < returnDuration; t += Time.unscaledDeltaTime)
            {
                transform.position = Vector3.Lerp(startPos, _originalPos, t / returnDuration);
                transform.rotation = Quaternion.Slerp(startRot, _originalRot, t / returnDuration);
                yield return null;    
            }

            transform.position = _originalPos;
            transform.rotation = _originalRot;
            Instantiate(returnEffect, transform.position, Quaternion.identity);
        }
    }
}
