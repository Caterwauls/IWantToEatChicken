using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class EEHurtEffect : MonoBehaviour
{
    public ERPlayer player;
    public float decaySpeed = 5f;
    
    private PostProcessVolume _volume;
    private CinemachineImpulseSource _impulse;
    private float _currentWeight = 0;
    
    private void Awake()
    {
        _volume = GetComponent<PostProcessVolume>();
        _impulse = GetComponent<CinemachineImpulseSource>();
        player.onTakeDamage += PlayEffect;
    }

    private void Update()
    {
        _currentWeight = Mathf.MoveTowards(_currentWeight, 0, Time.deltaTime * decaySpeed);
        _volume.weight = _currentWeight;
        if (Input.GetKeyDown(KeyCode.Q)) PlayEffect();
    }

    private void PlayEffect()
    {
        _currentWeight = 1f;
        _impulse.GenerateImpulse();
    }
}
