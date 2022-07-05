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
        var desiredWeight = 0f;
        if (player.health < player.maxHealth / 2f)
        {
            desiredWeight = (1f - player.health / player.maxHealth) - 0.5f;
        }
        _currentWeight = Mathf.MoveTowards(_currentWeight, desiredWeight, Time.deltaTime * decaySpeed);
        _volume.weight = _currentWeight;
    }

    private void PlayEffect()
    {
        _currentWeight = 1f;
        _impulse.GenerateImpulse();
    }
}
