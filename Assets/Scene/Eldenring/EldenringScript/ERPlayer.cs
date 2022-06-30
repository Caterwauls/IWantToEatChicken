using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ERPlayer : MonoBehaviour
{
    public bool isChanneling => _currentChannelTime > 0;
    public bool isStunned => _currentStunTime > 0;
    public bool canChannel => !isChanneling && !isStunned;
    
    private float _currentChannelTime = 0f;
    private float _currentStunTime = 0f;
    
    public void StartChannel(float preDelay, float postDelay = 0f, Action onComplete = null, Action onCancel = null)
    {
        if (isStunned || isChanneling) throw new Exception("Player cannot channel now");
        float fullChannelTime = preDelay + postDelay;
        _currentChannelTime = fullChannelTime;
        StartCoroutine(ChannelRoutine());
        IEnumerator ChannelRoutine()
        {
            float elapsedTime = 0;
            while (elapsedTime > preDelay)
            {
                if (isStunned)
                {
                    _currentChannelTime = 0f;
                    onCancel?.Invoke();
                    yield break;
                }
                yield return null;
                _currentChannelTime -= Time.deltaTime;
                elapsedTime = fullChannelTime - _currentChannelTime;
            }
            onComplete?.Invoke();
            while (_currentChannelTime > 0)
            {
                if (isStunned)
                {
                    _currentChannelTime = 0f;
                    yield break;
                }
                _currentChannelTime -= Time.deltaTime;
                yield return null;
            }
            _currentChannelTime = 0f;
        }
    }

    public void GrantInvincible(float duration)
    {
        // Not Implemented
    }
}
