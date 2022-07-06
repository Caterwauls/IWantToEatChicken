using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ERBoss : EREnemy
{
    public GameObject boomEffect;
    
    protected override void OnDeath()
    {
        StartCoroutine(Routine());
        IEnumerator Routine()
        {
            if (deathEffectPrefab != null)
                Instantiate(deathEffectPrefab, transform.position + Vector3.up, transform.rotation);
            foreach (var col in GetComponentsInChildren<Collider>())
            {
                col.enabled = false;
            }
            BGMManager.instance.

            GetComponent<Rigidbody>().isKinematic = true;
            var player = FindObjectOfType<ERPlayer>();
            player.ApplyHeal(player.maxHealth - player.health);
            ERUIManager.instance.WhiteFadeOut();
            yield return new WaitForSeconds(2.5f);
            foreach (var ren in GetComponentsInChildren<Renderer>())
            {
                ren.enabled = false;
            }
            if (boomEffect != null)
                Instantiate(boomEffect, transform.position + Vector3.up, transform.rotation);
            ERUIManager.instance.WhiteFadeIn();
            Destroy(gameObject);
        }
    }
}
