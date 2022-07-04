using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class ERScaleLightIntensity : MonoBehaviour
{
    public float intensity;
    private Light _light;

    private void Awake()
    {
        _light = GetComponent<Light>();
    }

    private void Update()
    {
        if (_light == null) _light = GetComponent<Light>();
        if (transform.lossyScale.x < 0.01f)
        {
            _light.enabled = false;
            return;
        }

        _light.enabled = true;
        _light.intensity = transform.lossyScale.x * intensity;
    }
}
