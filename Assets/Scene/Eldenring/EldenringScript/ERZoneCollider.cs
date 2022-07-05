using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ERZoneCollider : MonoBehaviour
{
    public string zoneName;
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<ERPlayer>() == null) return;
        ERGameManager.instance.EnterZone(zoneName);
    }
}
