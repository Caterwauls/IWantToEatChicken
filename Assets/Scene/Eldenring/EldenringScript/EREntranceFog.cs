using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EREntranceFog : ERInteractable
{
    public ParticleSystem enterEffect;
    public Transform destination;
    public GameObject[] objectsToDeactivate;
    public GameObject[] objectsToActivate;
    
    public override bool CanInteract(ERPlayer player)
    {
        return true;
    }

    public override string GetInteractText()
    {
        return "Enter The Fog";
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
            for (int i = 0; i < objectsToDeactivate.Length; i++)
            {
                if (objectsToDeactivate[i] == null) continue;
                objectsToDeactivate[i].SetActive(false);
            }
            for (int i = 0; i < objectsToActivate.Length; i++)
            {
                if (objectsToActivate[i] == null) continue;
                objectsToActivate[i].SetActive(true);
            }
            player.transform.position = destination.position;
            yield return new WaitForSeconds(0.5f);
            ERUIManager.instance.FadeIn();
        }
    }
}
