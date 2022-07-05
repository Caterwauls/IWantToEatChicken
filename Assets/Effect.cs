using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using Random = UnityEngine.Random;

public class Effect : MonoBehaviour
{
    private const float MinimumPlayDuration = 3f;
    
    public bool playOnStart = false;
    public bool destroyOnEnd = true;
    public bool randomizeAudioPitch = false;
    public float audioPitchMin = 0.9f;
    public float audioPitchMax = 1.1f;

    private bool _waitingForDestroy = false;
    private List<Func<bool>> _watchedConditions = new List<Func<bool>>();
    private float _lastPlayTime = 0f;

    private void Start()
    {
        if (playOnStart) Play();
    }

    public void Play()
    {
        if (destroyOnEnd) _waitingForDestroy = true;
        _watchedConditions.Clear();
        _lastPlayTime = Time.time;
        
        foreach (var ps in GetComponentsInChildren<ParticleSystem>())
        {
            ps.Play();
            var thisPs = ps;
            if (destroyOnEnd) 
                _watchedConditions.Add(() => thisPs == null || !thisPs.IsAlive());
        }

        foreach (var au in GetComponentsInChildren<AudioSource>())
        {
            if (randomizeAudioPitch) 
                au.pitch = Random.Range(audioPitchMin, audioPitchMax);
            au.Play();
            var thisAu = au;
            if (destroyOnEnd) 
                _watchedConditions.Add(() => thisAu == null || !thisAu.isPlaying);
        }

        foreach (var ci in GetComponentsInChildren<CinemachineImpulseSource>())
        {
            ci.GenerateImpulse();
        }
    }

    #if UNITY_EDITOR
    private void OnValidate()
    {
        foreach (var au in GetComponentsInChildren<AudioSource>())
        {
            if (!au.playOnAwake) continue;
            au.playOnAwake = false;
            UnityEditor.EditorUtility.SetDirty(au);
        }
    }
    #endif

    private void FixedUpdate()
    {
        if (!_waitingForDestroy) return;
        if (Time.time - _lastPlayTime < MinimumPlayDuration) return;

        foreach (var cond in _watchedConditions)
        {
            if (!cond()) return;
        }

        Destroy(gameObject);
    }
}
