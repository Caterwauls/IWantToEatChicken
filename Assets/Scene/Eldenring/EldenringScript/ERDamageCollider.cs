using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ERDamageCollider : MonoBehaviour
{
    public Collider selfCollider;
    public float damageMin = 10;
    public float damageMax = 20;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other == selfCollider) return;
        if (other.TryGetComponent(out EREntity entity))
        {
            entity.ApplyDamage(Random.Range(damageMin, damageMax));
        }
    }
}
