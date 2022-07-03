using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ERPlayer : EREntity
{
    public float stamina { get; private set; } = 100;
    public float maxStamina = 100;
    public float staminaChargeDelay = 1f;
    public float staminaChargeSpeed = 30f;
    
    private float _lastStaminaUseTime;
    
    public void UseStamina(float amount)
    {
        stamina = Mathf.MoveTowards(stamina, 0, amount);
        _lastStaminaUseTime = Time.time;
    }

    protected override void Update()
    {
        base.Update();
        if (Time.time - _lastStaminaUseTime > staminaChargeDelay)
        {
            stamina = Mathf.MoveTowards(stamina, maxStamina, staminaChargeSpeed * Time.deltaTime);
        }
    }

    protected override void OnDeath()
    {
        base.OnDeath();
        StartCoroutine(Routine());

        IEnumerator Routine()
        {
            yield return new WaitForSeconds(1.5f);
            ERUIManager.instance.ShowYouDiedMessage();
            yield return new WaitForSeconds(4.5f);
            ERUIManager.instance.FadeOut();
            yield return new WaitForSeconds(2.0f);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
