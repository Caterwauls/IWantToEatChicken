using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EREntranceFog : ERInteractable
{
    public ParticleSystem enterEffect;
    public Transform destination;
    
    public override bool CanInteract(ERPlayer player)
    {
        return true;
    }

    public override string GetInteractText()
    {
        return "안개 속으로 들어간다";
    }

    public override void Interact(ERPlayer player)
    {
        base.Interact(player);
        StartCoroutine(Routine());
        IEnumerator Routine()
        {
            player.StartChannel(3f);
            enterEffect.Play();
            yield return new WaitForSeconds(0.5f);
            ERUIManager.instance.FadeOut();
            yield return new WaitForSeconds(1);
            player.transform.position = destination.position;
            yield return new WaitForSeconds(0.5f);
            ERUIManager.instance.FadeIn();
        }
    }
}
