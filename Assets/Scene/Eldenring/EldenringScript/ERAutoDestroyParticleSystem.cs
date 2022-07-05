using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ERAutoDestroyParticleSystem : MonoBehaviour
{
    private ParticleSystem _ps;
 
    private void Awake()
    {
        _ps = GetComponent<ParticleSystem>();
        if (_ps == null) Destroy(gameObject);
    }

    private void Update()
    {
        if (_ps.IsAlive()) return;
        Destroy(gameObject);
    }
}
