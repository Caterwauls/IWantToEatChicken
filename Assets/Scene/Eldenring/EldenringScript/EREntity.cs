using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EREntity : MonoBehaviour
{
    public bool isChanneling => _currentChannelTime > 0;
    public bool isStunned => _currentStunTime > 0;
    public bool canChannel => !isChanneling && !isStunned && isAlive;
    public bool isInvincible => _currentInvincibleTime > 0;

    public Action onTakeDamage;
    
    public float health { get; private set; } = 100;
    public float maxHealth = 100;

    public bool isAlive { get; private set; } = true;
    public bool isDead => !isAlive;

    public GameObject damageEffectPrefab;
    public GameObject deathEffectPrefab;

    public bool canBeStunned = true;

    private float _currentChannelTime = 0f;
    private float _currentStunTime = 0f;
    private float _currentInvincibleTime = 0;

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
            while (elapsedTime < preDelay)
            {
                if (isStunned || isDead)
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
                if (isStunned || isDead)
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
        _currentInvincibleTime = Mathf.MoveTowards(_currentInvincibleTime, 0, Time.deltaTime);
    }

    public void GrantInvincible(float duration)
    {
        _currentInvincibleTime = Mathf.Max(_currentInvincibleTime, duration);
    }

    public void ApplyStun(float duration)
    {
        if (isInvincible) return;
        if (!canBeStunned) return;
        _currentStunTime = Mathf.Max(_currentStunTime, duration);
    }
    
    public void ApplyDamage(float amount)
    {
        if (isInvincible) return;
        health -= amount;
        onTakeDamage?.Invoke();
        if (damageEffectPrefab != null) 
            Instantiate(damageEffectPrefab, transform.position + Vector3.up, transform.rotation);
        if (health <= 0)
        {
            OnDeath();
            isAlive = false;
        }
    }

    protected virtual void OnDeath()
    {
        if (deathEffectPrefab != null)
            Instantiate(deathEffectPrefab, transform.position + Vector3.up, transform.rotation);
        foreach (var ren in GetComponentsInChildren<Renderer>())
        {
            ren.enabled = false;
        }
        foreach (var col in GetComponentsInChildren<Collider>())
        {
            col.enabled = false;
        }

        GetComponent<Rigidbody>().isKinematic = true;
    }

    public void ApplyHeal(float amount)
    {
        health = Mathf.MoveTowards(health, maxHealth, amount);
    }
}
