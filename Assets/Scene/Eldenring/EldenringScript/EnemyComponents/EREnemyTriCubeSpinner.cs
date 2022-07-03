using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EREnemyTriCubeSpinner : EREnemyComponent
{
    public Transform[] cubeTransforms;
    public float[] desiredVels;

    public float onHitVelRandomMin = -800;
    public float onHitVelRandomMax = 800;
    public float idleVelChangeSpeed = 300;

    private float[] _currentVels;
    
    private void Start()
    {
        _currentVels = new float[desiredVels.Length];
        _enemy.onTakeDamage += () =>
        {
            for (int i = 0; i < _currentVels.Length; i++)
            {
                _currentVels[i] = Random.Range(onHitVelRandomMin, onHitVelRandomMax);
            }
        };
    }

    private void Update()
    {
        for (int i = 0; i < cubeTransforms.Length; i++)
        {
            _currentVels[i] = Mathf.MoveTowards(_currentVels[i], desiredVels[i], Time.deltaTime * idleVelChangeSpeed);
            cubeTransforms[i].Rotate(0, _currentVels[i] * Time.deltaTime, 0);
        }
    }
}
