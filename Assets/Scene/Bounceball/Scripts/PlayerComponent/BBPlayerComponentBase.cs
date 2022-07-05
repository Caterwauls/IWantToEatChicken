using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BBPlayerComponentBase : MonoBehaviour
{
    protected BBPlayer _player;
    protected Rigidbody _rb;

    protected virtual void Awake()
    {
        _player = GetComponent<BBPlayer>();
        _rb = GetComponent<Rigidbody>();
    }
}
