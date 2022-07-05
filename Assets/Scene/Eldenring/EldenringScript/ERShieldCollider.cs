using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ERShieldCollider : MonoBehaviour
{
    public GameObject blockEffect;
    public System.Action<float> onBlock;

    public void NotifyBlock(float damage)
    {
        onBlock?.Invoke(damage);
        Instantiate(blockEffect, transform.position, transform.rotation);
    }
}
