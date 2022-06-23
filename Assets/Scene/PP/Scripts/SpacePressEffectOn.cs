using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpacePressEffectOn : MonoBehaviour
{
    public bool canPlayEffect;
    private ParticleSystem _pressEffect;

    private void Awake()
    {
        canPlayEffect = false;
        _pressEffect = gameObject.GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canPlayEffect)
        {
            _pressEffect.Play();
        }
        else
        {
            _pressEffect.Stop();
        }
    }
}
