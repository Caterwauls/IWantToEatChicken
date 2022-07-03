using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EREnemyComponent : MonoBehaviour
{
    protected Rigidbody _rb;
    protected EREnemy _enemy;

    protected virtual void Awake()
    {
        UseComponent(out _rb);
        UseComponent(out _enemy);
    }

    protected void UseComponent<T>(out T variable) where T : Component
    {
        variable = GetComponent<T>();
    }
}
