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

    public CanvasGroup newZoneFoundMessageGroup;
    public Text newZoneNameText;

    public CanvasGroup youDiedMessageGroup;
    public Transform youDiedTextTransform;

    public CanvasGroup interactGroup;
    public ERPlayer interactPlayer;
    public float interactDistance;
    public Text interactText;
    
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
                
                if (Input.GetKeyDown(KeyCode.E)) interactable.Interact(interactPlayer);
                text = interactable.GetInteractText();
            }
        }
        
        if (text == null)
        {
            interactGroup.alpha = Mathf.MoveTowards(interactGroup.alpha, 0, Time.deltaTime * 4f);
        }
        else
        {
            interactText.text = text;
            interactGroup.alpha = Mathf.MoveTowards(interactGroup.alpha, 1, Time.deltaTime * 4f);
        }
    }

    public void FadeIn()
    {
        StartCoroutine(FadeInRoutine());
        IEnumerator FadeInRoutine()
        {
            for (float t = 0; t < 1; t += Time.deltaTime)
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
            for (float t = 0; t < 1; t += Time.deltaTime)
            {
                fadeEffectGroup.alpha = t;
                yield return null;
            }
            fadeEffectGroup.alpha = 1;
        }
    }
    
    public void ShowSavedMessage()
    {
        StartCoroutine(Routine());
        IEnumerator Routine()
        {
            savedMessageGroup.gameObject.SetActive(true);
            for (float t = 0; t < 1; t += Time.deltaTime)
            {
                savedMessageGroup.alpha = t;
                dimmerTextTransform.transform.localScale = new Vector3(0.9f + t * 0.3f, 1, 1);
                yield return null;
            }

            yield return new WaitForSeconds(3f);
            for (float t = 0; t < 1; t += Time.deltaTime)
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
            newZoneNameText.text = zoneName;
            for (float t = 0; t < 1; t += Time.deltaTime)
            {
                newZoneFoundMessageGroup.alpha = t;
                yield return null;
            }

            yield return new WaitForSeconds(3f);
            for (float t = 0; t < 1; t += Time.deltaTime)
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
            for (float t = 0; t < 1; t += Time.deltaTime)
            {
                youDiedMessageGroup.alpha = t;
                youDiedTextTransform.localScale = Vector3.one * (0.9f + t * 0.1f);
                yield return null;
            }

            yield return new WaitForSeconds(2f);
            for (float t = 0; t < 1; t += Time.deltaTime)
            {
                youDiedMessageGroup.alpha = 1 - t;
                youDiedTextTransform.localScale = Vector3.one * (1.0f + t * 0.1f);
                yield return null;
            }

            youDiedMessageGroup.gameObject.SetActive(false);
        }
    }
}
