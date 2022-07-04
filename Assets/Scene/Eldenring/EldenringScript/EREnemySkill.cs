using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EREnemySkill : MonoBehaviour
{
    public float cooldownTime = 4f;
    public float targetRange = 5f;
    public float channelTime = 1f;
    public float postDelay = 2f;

    public Effect fxOnChannelStart;
    public Effect fxOnChannelComplete;
    public Effect fxOnChannelCancel;

    protected EREnemy _self { get; private set; }

    private float _lastSkillUseTime = float.NegativeInfinity;
    private Coroutine _skillUseCheckRoutine;
    
    protected virtual void Awake()
    {
        _self = GetComponentInParent<EREnemy>();
    }

    private void OnEnable()
    {
        _skillUseCheckRoutine = StartCoroutine(SkillUseCheckRoutine());

        IEnumerator SkillUseCheckRoutine()
        {
            while (true)
            {
                if (!_self.canChannel)
                {
                    yield return new WaitForSeconds(Random.Range(0.15f, 0.35f));
                    continue;
                }
                if (Time.time - _lastSkillUseTime > cooldownTime && 
                    _self.target != null && 
                    Vector3.Distance(_self.transform.position, _self.target.transform.position) < targetRange)
                {
                    _self.StartChannel(channelTime, postDelay, OnChannelComplete, OnChannelCancel);
                    _lastSkillUseTime = Time.time;
                    OnChannelStart();
                }
                yield return new WaitForSeconds(Random.Range(0.15f, 0.35f));
            }
        }
    }

    private void OnDisable()
    {
        StopCoroutine(_skillUseCheckRoutine);
    }

    protected virtual void OnChannelStart()
    {
        if (fxOnChannelStart != null) fxOnChannelStart.Play();
    }
    
    protected virtual void OnChannelCancel()
    {
        if (fxOnChannelCancel != null) fxOnChannelCancel.Play();
    }
    
    protected virtual void OnChannelComplete()
    {
        if (fxOnChannelComplete != null) fxOnChannelComplete.Play();
    }
}
