using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ERUIManager : MonoBehaviour
{
    public static ERUIManager instance
    {
        get
        {
            if (_instance == null) _instance = FindObjectOfType<ERUIManager>();
            return _instance;
        }
    }
    private static ERUIManager _instance;

    public CanvasGroup savedMessageGroup;
    public Transform dimmerTextTransform;

    public CanvasGroup fadeEffectGroup;
    public CanvasGroup whiteFadeEffectGroup;

    public CanvasGroup newZoneFoundMessageGroup;
    public Text newZoneNameText;
    public AudioSource newZoneFoundAudio;

    public CanvasGroup youDiedMessageGroup;
    public Transform youDiedTextTransform;
    public AudioSource youDiedAudio;

    public CanvasGroup interactGroup;
    public ERPlayer interactPlayer;
    public float interactDistance;
    public Text interactText;
    public AudioSource interactAudio;

    public void PlayInteractSound()
    {
        interactAudio.Play();
    }
    
    private void Update()
    {
        string text = null;
        if (interactPlayer.canChannel)
        {
            var cols = Physics.OverlapSphere(interactPlayer.transform.position, interactDistance);
            foreach (var col in cols)
            {
                if (!col.TryGetComponent(out ERInteractable interactable)) continue;
                if (!interactable.CanInteract(interactPlayer)) continue;

                if (Input.GetKeyDown(KeyCode.E))
                {
                    interactable.Interact(interactPlayer);
                    interactAudio.Play();
                }
                text = interactable.GetInteractText();
            }
        }
        
        if (text == null)
        {
            interactGroup.alpha = Mathf.MoveTowards(interactGroup.alpha, 0, Time.unscaledDeltaTime * 4f);
        }
        else
        {
            interactText.text = text;
            interactGroup.alpha = Mathf.MoveTowards(interactGroup.alpha, 1, Time.unscaledDeltaTime * 4f);
        }
    }

    public void FadeIn()
    {
        StartCoroutine(FadeInRoutine());
        IEnumerator FadeInRoutine()
        {
            for (float t = 0; t < 1; t += Time.unscaledDeltaTime)
            {
                fadeEffectGroup.alpha = 1 - t;
                yield return null;
            }
            fadeEffectGroup.alpha = 0;
        }
    }

    public void FadeOut()
    {
        StartCoroutine(FadeOutRoutine());
        IEnumerator FadeOutRoutine()
        {
            for (float t = 0; t < 1; t += Time.unscaledDeltaTime)
            {
                fadeEffectGroup.alpha = t;
                yield return null;
            }
            fadeEffectGroup.alpha = 1;
        }
    }

    public void WhiteFadeIn()
    {
        StartCoroutine(FadeInRoutine());
        IEnumerator FadeInRoutine()
        {
            for (float t = 0; t < 1; t += Time.unscaledDeltaTime * 0.5f)
            {
                whiteFadeEffectGroup.alpha = 1 - t;
                yield return null;
            }
            whiteFadeEffectGroup.alpha = 0;
        }
    }

    public void WhiteFadeOut()
    {
        StartCoroutine(FadeOutRoutine());
        IEnumerator FadeOutRoutine()
        {
            for (float t = 0; t < 1; t += Time.unscaledDeltaTime * 0.5f)
            {
                whiteFadeEffectGroup.alpha = t;
                yield return null;
            }
            whiteFadeEffectGroup.alpha = 1;
        }
    }
    
    public void ShowSavedMessage()
    {
        StartCoroutine(Routine());
        IEnumerator Routine()
        {
            savedMessageGroup.gameObject.SetActive(true);
            for (float t = 0; t < 1; t += Time.unscaledDeltaTime)
            {
                savedMessageGroup.alpha = t;
                dimmerTextTransform.transform.localScale = new Vector3(0.9f + t * 0.3f, 1, 1);
                yield return null;
            }

            yield return new WaitForSeconds(3f);
            for (float t = 0; t < 1; t += Time.unscaledDeltaTime)
            {
                savedMessageGroup.alpha = 1 - t;
                yield return null;
            }

            savedMessageGroup.gameObject.SetActive(false);
        }
    }

    public void ShowNewZoneFoundMessage(string zoneName)
    {
        StartCoroutine(Routine());
        IEnumerator Routine()
        {
            newZoneFoundMessageGroup.gameObject.SetActive(true);
            newZoneFoundAudio.Play();
            newZoneNameText.text = zoneName;
            for (float t = 0; t < 1; t += Time.unscaledDeltaTime)
            {
                newZoneFoundMessageGroup.alpha = t;
                yield return null;
            }

            yield return new WaitForSeconds(1.5f);
            for (float t = 0; t < 1; t += Time.unscaledDeltaTime)
            {
                newZoneFoundMessageGroup.alpha = 1 - t;
                yield return null;
            }

            newZoneFoundMessageGroup.gameObject.SetActive(false);
        }
    }

    public void ShowYouDiedMessage()
    {
        StartCoroutine(Routine());
        IEnumerator Routine()
        {
            youDiedMessageGroup.gameObject.SetActive(true);
            youDiedAudio.Play();
            for (float t = 0; t < 1; t += Time.unscaledDeltaTime)
            {
                youDiedMessageGroup.alpha = t;
                youDiedTextTransform.localScale = Vector3.one * (0.9f + t * 0.1f);
                yield return null;
            }

            yield return new WaitForSeconds(2f);
            for (float t = 0; t < 1; t += Time.unscaledDeltaTime)
            {
                youDiedMessageGroup.alpha = 1 - t;
                youDiedTextTransform.localScale = Vector3.one * (1.0f + t * 0.1f);
                yield return null;
            }

            youDiedMessageGroup.gameObject.SetActive(false);
        }
    }
}
