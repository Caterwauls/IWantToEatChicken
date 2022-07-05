using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ERPlayerComponent : MonoBehaviour
{
    protected Rigidbody _rb;
    protected ERPlayer _player;

    protected virtual void Awake()
    {
        UseComponent(out _rb);
        UseComponent(out _player);
    }

    protected void UseComponent<T>(out T variable) where T : Component
    {
        variable = GetComponent<T>();
    }
}
