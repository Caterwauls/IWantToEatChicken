using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ERSavePoint : ERInteractable
{
    public bool didSaveHere => Vector3.Distance(ERGameManager.savePosition, transform.position + savePointOffset) < 1f;

    public Transform animatedCube;
    public Effect explodeEffect;
    private Vector3 _animatedCubeStartPos;
    public Vector3 savePointOffset = Vector3.up;
    
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
        return didSaveHere ? "Rest here" : "Touch The Hexahedron";
    }

    public override void Interact(ERPlayer player)
    {
        ERGameManager.instance.onSavePointUse?.Invoke();
        player.GetComponent<ERPlayerAttack>().HolsterSword();
        if (didSaveHere)
        {
            StartCoroutine(HealRoutine());
            return;
        }
        
        StartCoroutine(SaveRoutine());
        
        IEnumerator SaveRoutine()
        {
            ERGameManager.savePosition = transform.position + savePointOffset;
            player.GetComponent<ERPlayerPotion>().RechargePotions();
            player.StartChannel(3.5f);
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
            player.GetComponent<ERPlayerPotion>().RechargePotions();
            while (player.health < player.maxHealth)
            {
                player.ApplyHeal(50 * Time.deltaTime);
                yield return null;
            }
        }
    }
}
