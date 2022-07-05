using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ERPlayerPotion : ERPlayerComponent
{
    public GameObject channelStartEffect;
    public GameObject channelEndEffect;
    public int currentPotion { get; private set; }
    public int maxPotion = 3;
    public float channelTime;
    public float postDelay;
    public float potionHealAmount;
    public float potionHealDuration;

    protected override void Awake()
    {
        base.Awake();
        currentPotion = maxPotion;
    }

    private void Update()
    {
        if (!_player.canChannel) return;
        if (currentPotion <= 0) return;
        if (_player.health + 1 >= _player.maxHealth) return;
        if (Input.GetKeyDown(KeyCode.R))
        {
            Instantiate(channelStartEffect, transform);
            _player.StartChannel(channelTime, postDelay, () =>
            {
                currentPotion--;
                Instantiate(channelEndEffect, transform);
                StartCoroutine(HealRoutine());
            });
        }
    }

    private IEnumerator HealRoutine()
    {
        for (float t = 0; t < potionHealDuration; t += Time.deltaTime)
        {
            if (_player.isDead) yield break;
            _player.ApplyHeal(potionHealAmount / potionHealDuration * Time.deltaTime);
            yield return null;
        }
    }

    public void RechargePotions()
    {
        currentPotion = maxPotion;
    }
}
