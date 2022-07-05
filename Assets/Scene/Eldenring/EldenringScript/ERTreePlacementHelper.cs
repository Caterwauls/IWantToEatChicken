using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[ExecuteAlways]
public class ERTreePlacementHelper : MonoBehaviour
{
    [SerializeField, HideInInspector] private Vector3 _lastPosition;

    public bool randomizeScale = true;
    public bool randomizeRotation = true;
    
    private void Update()
    {
        if (Application.IsPlaying(this))
        {
            Destroy(this);
        }
        
        if (Vector3.SqrMagnitude(_lastPosition - transform.position) > 1)
        {
            RandomizePos();
            _lastPosition = transform.position;
        }
    }

    private void RandomizePos()
    {
        if (randomizeRotation) transform.rotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
        if (randomizeScale) transform.localScale = Vector3.one * Random.Range(0.8f, 1.2f);
    }
}
