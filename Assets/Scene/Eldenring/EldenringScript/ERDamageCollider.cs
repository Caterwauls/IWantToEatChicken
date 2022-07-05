using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ERDamageCollider : MonoBehaviour
{
    public EREntity selfEntity;
    public float damageMin = 10;
    public float damageMax = 20;
    public float pushVelocity = 5;
    public float verticalPushVelocity = 0;
    public float stunDuration = 0.35f;
    public Action<float> onAttackBlocked;

    public bool canDamageEnemy;
    public bool canDamagePlayer;

    public float damageCooldown = 0.3f;

    private float _lastDamageTime = float.NegativeInfinity;
    
    private void OnTriggerEnter(Collider other)
    {
        var shield = other.GetComponent<ERShieldCollider>();
        if (shield != null && shield.enabled)
        {
            var amount = Random.Range(damageMin, damageMax);
            shield.NotifyBlock(amount);
            onAttackBlocked?.Invoke(amount);
            return;
        }
        
        var ent = other.GetComponent<EREntity>();
        if (ent == null || ent == selfEntity || ent.isInvincible) return;
        if (!canDamageEnemy && ent is EREnemy) return;
        if (!canDamagePlayer && ent is ERPlayer) return;
        if (Time.time - _lastDamageTime < damageCooldown) return;
        _lastDamageTime = Time.time;
        ent.ApplyDamage(Random.Range(damageMin, damageMax));
        ent.ApplyStun(stunDuration);
        var pushDir = (other.transform.position - transform.position).normalized;
        ent.GetComponent<Rigidbody>().velocity += pushDir * pushVelocity + Vector3.up * verticalPushVelocity;
    }
}
