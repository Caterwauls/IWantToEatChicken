using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ERSavePoint : ERInteractable
{
    public bool didSaveHere => Vector3.Distance(ERGameManager.savePosition, transform.position) > 1f;

    public Transform animatedCube;
    public ParticleSystem explodeEffect;
    private Vector3 _animatedCubeStartPos;
    
    private void Awake()
    {
        _animatedCubeStartPos = animatedCube.position;
    }

    private void Update()
    {
        animatedCube.position = _animatedCubeStartPos + Mathf.Sin(Time.time * 1.5f) * Vector3.up * 0.5f;
        animatedCube.rotation = Quaternion.Euler(0, Time.time * 90, 0);
    }

    public override bool CanInteract(ERPlayer player)
    {
        return true;
    }

    public override string GetInteractText()
    {
        return didSaveHere ? "여기서 쉰다" : "육면체에 닿는다";
    }

    public override void Interact(ERPlayer player)
    {
        if (didSaveHere)
        {
            player.StartChannel(2f);
            StartCoroutine(HealRoutine());
            return;
        }
        
        StartCoroutine(SaveRoutine());
        
        IEnumerator SaveRoutine()
        {
            ERGameManager.savePosition = transform.position;
            player.StartChannel(4.5f);
            yield return new WaitForSeconds(1.5f);
            explodeEffect.Play();
            ERUIManager.instance.ShowSavedMessage();
            while (player.health < player.maxHealth)
            {
                player.ApplyHeal(50 * Time.deltaTime);
                yield return null;
            }
        }
        
        IEnumerator HealRoutine()
        {
            player.StartChannel(1.5f);
            while (player.health < player.maxHealth)
            {
                player.ApplyHeal(50 * Time.deltaTime);
                yield return null;
            }
        }
    }
}
