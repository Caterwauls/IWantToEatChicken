using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ERUIHeroBar : MonoBehaviour
{
    public ERPlayer player;

    public Image health;
    public Image healthDelta;
    public Image stamina;
    public Image staminaDelta;
    
    public float deltaDelay = 1.0f;
    public float deltaDecaySpeed = 0.3f;

    private float _lastHealthFillAmount;
    private float _lastStaminaFillAmount;
    private float _currentHealthDeltaDelay;
    private float _currentStaminaDeltaDelay;
    
    private void Update()
    {
        health.fillAmount = player.health / player.maxHealth;
        stamina.fillAmount = player.stamina / player.maxStamina;

        if (Math.Abs(healthDelta.fillAmount - health.fillAmount) < 0.001f)
            _currentHealthDeltaDelay = deltaDelay;
        else
            _currentHealthDeltaDelay = Mathf.MoveTowards(_currentHealthDeltaDelay, 0, Time.deltaTime);
        
        if (Math.Abs(staminaDelta.fillAmount - stamina.fillAmount) < 0.001f)
            _currentStaminaDeltaDelay = deltaDelay;
        else
            _currentStaminaDeltaDelay = Mathf.MoveTowards(_currentStaminaDeltaDelay, 0, Time.deltaTime);

        
        if (health.fillAmount > healthDelta.fillAmount)
            healthDelta.fillAmount = health.fillAmount;

        if (stamina.fillAmount > staminaDelta.fillAmount)
            staminaDelta.fillAmount = stamina.fillAmount;
        
        if (_currentHealthDeltaDelay < 0.001f)
        {
            healthDelta.fillAmount = Mathf.MoveTowards(healthDelta.fillAmount, health.fillAmount,
                deltaDecaySpeed * Time.deltaTime);
        }

        if (_currentStaminaDeltaDelay < 0.001f)
        {
            staminaDelta.fillAmount = Mathf.MoveTowards(staminaDelta.fillAmount, stamina.fillAmount,
                deltaDecaySpeed * Time.deltaTime);
        }
    }
}
