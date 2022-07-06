using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BBAbilityOrb : MonoBehaviour
{
    public GameObject absorbEffect;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player") return;
        OnAbsorb(other.transform.GetComponent<BBPlayer>());
        Instantiate(absorbEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    protected abstract void OnAbsorb(BBPlayer player);

    private void OnDrawGizmos()
    {
        var col = GetComponent<SphereCollider>();
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(col.center + transform.position, col.radius);
    }
}
