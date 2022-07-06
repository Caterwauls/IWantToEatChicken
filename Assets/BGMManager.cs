using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BGMManager : MonoBehaviour
{
    public static BGMManager instance
    {
        get
        {
            if (_instance == null) _instance = FindObjectOfType<BGMManager>();
            return _instance;
        }
    }
    private static BGMManager _instance;
    
    public AudioClip desiredClip;

    private AudioSource _source;

    private void Awake()
    {
        _source = GetComponent<AudioSource>();
        if (instance != null && instance != this)
        {
            instance.desiredClip = desiredClip;
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (desiredClip != _source.clip)
        {
            _source.volume = Mathf.MoveTowards(_source.volume, 0, Time.deltaTime / 2f);
            if (_source.volume < 0.01f || _source.clip == null)
            {
                _source.clip = desiredClip;
                _source.Play();
            }
        }
        else
        {
            _source.volume = Mathf.MoveTowards(_source.volume, 1, Time.deltaTime);
        }
    }
}
