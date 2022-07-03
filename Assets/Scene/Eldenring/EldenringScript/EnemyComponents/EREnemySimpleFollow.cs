using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EREnemySimpleFollow : EREnemyComponent
{
    public float followSpeed = 5f;
    public float desiredDistance = 2.5f;

    public float offsetMin = 4f;
    public float offsetMax = 8f;
    public float offsetResetIntervalMin = 2f;
    public float offsetResetIntervalMax = 6f;
    private Vector3 _currentOffset;

    private void Start()
    {
        StartCoroutine(OffsetRoutine());

        IEnumerator OffsetRoutine()
        {
            _currentOffset = Random.insideUnitSphere;
            _currentOffset.y = 0;
            _currentOffset.Normalize();
            _currentOffset *= Random.Range(offsetMin, offsetMax);
            yield return new WaitForSeconds(Random.Range(offsetResetIntervalMin, offsetResetIntervalMax));
        }
    }

    private void Update()
    {
        if (!_enemy.canChannel) return;
        if (_enemy.target == null) return;
        var desiredPos = _enemy.target.transform.position + _currentOffset;
        if (Vector3.Distance(desiredPos, transform.position) < desiredDistance) return;
        var moveDir = desiredPos - transform.position;
        moveDir.y = 0;
        moveDir.Normalize();
        transform.position += moveDir * followSpeed * Time.deltaTime;
    }
}
