using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ERUIBossHealthBar : MonoBehaviour
{
   public EREnemy target;

    public Image health;
    public Image healthDelta;
    
    public float deltaDelay = 1.0f;
    public float deltaDecaySpeed = 0.25f;

    public float destroyAfterDeathTime = 3f;

    private float _lastHealthFillAmount;
    private float _lastStaminaFillAmount;
    private float _currentHealthDeltaDelay;
    private float _currentStaminaDeltaDelay;
    
    private bool _isMarkedForDestroy;

    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        if (target.isDead && !_isMarkedForDestroy)
        {
            _isMarkedForDestroy = true;
            Destroy(gameObject, destroyAfterDeathTime);
        }
        
        health.fillAmount = target.health / target.maxHealth;

        if (Mathf.Abs(healthDelta.fillAmount - health.fillAmount) < 0.001f)
            _currentHealthDeltaDelay = deltaDelay;
        else
            _currentHealthDeltaDelay = Mathf.MoveTowards(_currentHealthDeltaDelay, 0, Time.deltaTime);

        if (health.fillAmount > healthDelta.fillAmount)
            healthDelta.fillAmount = health.fillAmount;

        if (_currentHealthDeltaDelay < 0.001f)
        {
            healthDelta.fillAmount = Mathf.MoveTowards(healthDelta.fillAmount, health.fillAmount,
                deltaDecaySpeed * Time.deltaTime);
        }
    }
}
