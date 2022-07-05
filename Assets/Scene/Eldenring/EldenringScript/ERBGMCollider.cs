using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ERBGMCollider : MonoBehaviour
{
    public AudioClip clip;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<ERPlayer>() == null) return;
        BGMManager.instance.desiredClip = clip;
    }
}
