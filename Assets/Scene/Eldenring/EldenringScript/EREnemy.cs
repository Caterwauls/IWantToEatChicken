using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EREnemy : EREntity
{
    private static Collider[] _overlapBuffer = new Collider[128];
    
    public ERPlayer target { get; private set; }
    
    public float targetAcquisitionRange = 6f;
    public float targetLossRange = 10f;

    public Transform lockOnPosition;
    public Transform healthBarPosition;

    public bool isBoss = false;

    private Coroutine _acquireTargetRoutine;

    private void Start()
    {
        ERGameManager.instance.onEnemySpawn?.Invoke(this);
    }

    private void OnEnable()
    {
        _acquireTargetRoutine = StartCoroutine(AcquireTargetRoutine());
        IEnumerator AcquireTargetRoutine()
        {
            while (true)
            {
                if (isStunned)
                {
                    yield return new WaitForSeconds(1);
                    continue;
                }
                
                if (target == null)
                {
                    var count = Physics.OverlapSphereNonAlloc(transform.position, targetAcquisitionRange, _overlapBuffer);
                    for (int i = 0; i < count; i++)
                    {
                        var t = _overlapBuffer[i].GetComponentInParent<ERPlayer>();
                        if (t == null || t.isDead) continue;
                        target = t;
                        ERGameManager.instance.onEnemyTargetAcquisition?.Invoke();
                    }
                }
                else
                {
                    if (target.isDead || Vector3.Distance(transform.position, target.transform.position) >
                        targetLossRange)
                    {
                        target = null;
                    }
                }
                
                yield return new WaitForSeconds(Random.Range(1f, 2f));
            }
        }
    }

    private void OnDestroy()
    {
        if (ERGameManager.instance == null) return;
        ERGameManager.instance.onEnemyDestroy?.Invoke(this);
    }

    private void OnDisable()
    {
        StopCoroutine(_acquireTargetRoutine);
    }

    protected override void OnDeath()
    {
        base.OnDeath();
        Destroy(gameObject, 5f);
    }
}
