using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EREntity : MonoBehaviour
{
    public bool isChanneling => _currentChannelTime > 0;
    public bool isStunned => _currentStunTime > 0;
    public bool canChannel => !isChanneling && !isStunned;
    
    public float health { get; private set; } = 100;
    public float maxHealth = 100;

    public float stunDurationOnGetDamage = 0.35f;

    public GameObject deathEffectPrefab;

    private float _currentChannelTime = 0f;
    private float _currentStunTime = 0f;

    private void Awake()
    {
        health = maxHealth;
    }
    
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
                yield return null;
                if (isStunned)
                {
                    _currentChannelTime = 0f;
                    yield break;
                }
                _currentChannelTime -= Time.deltaTime;
            }
            _currentChannelTime = 0f;
        }
    }

    protected virtual void Update()
    {
        _currentStunTime = Mathf.MoveTowards(_currentStunTime, 0, Time.deltaTime);
    }

    public void GrantInvincible(float duration)
    {
        // Not Implemented
    }

    public void ApplyDamage(float amount)
    {
        health -= amount;
        _currentStunTime = Mathf.Max(_currentStunTime, stunDurationOnGetDamage);
        if (health <= 0)
        {
            OnDeath();
        }
    }

    protected virtual void OnDeath()
    {
        Instantiate(deathEffectPrefab, transform.position, transform.rotation);
        foreach (var ren in GetComponentsInChildren<Renderer>())
        {
            ren.enabled = false;
        }
    }

    public void ApplyHeal(float amount)
    {
        health = Mathf.MoveTowards(health, maxHealth, amount);
    }
}
