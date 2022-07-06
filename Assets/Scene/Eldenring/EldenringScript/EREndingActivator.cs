using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EREndingActivator : ERInteractable
{    
    public Transform animatedCube;
    public float startIntensity;
    public float endIntensity;
    public float duration;
    public float startRange;
    public float endRange;
    public Effect effect;
    public string nextSceneName;

    private Light _light;
    private Vector3 _animatedCubeStartPos;
    
    private void Awake()
    {
        _light = GetComponentInChildren<Light>();
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
        return "육면체에 닿는다";
    }

    public override void Interact(ERPlayer player)
    {
        StartCoroutine(Routine());
        
        IEnumerator Routine()
        {
            player.GetComponent<ERPlayerAttack>().HolsterSword();
            player.StartChannel(10f);
            effect.Play();
            for (float t = 0; t < duration; t += Time.deltaTime)
            {
                _light.intensity = Mathf.Lerp(startIntensity, endIntensity, t / duration);
                _light.range = Mathf.Lerp(startRange, endRange, t / duration);
                yield return null;
            }
            ERUIManager.instance.WhiteFadeOut();
            yield return new WaitForSeconds(5.5f);
            SceneManager.LoadScene(nextSceneName);
        }
    }
}
