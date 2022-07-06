using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BBCubenRing : MonoBehaviour
{
    public Effect explodeEffect;
    public CanvasGroup interactGroup;
    public CanvasGroup whiteFade;
    public AudioSource interactAudio;
    public float activationDist = 8f;
    private BBPlayer _player;

    private bool _didActivate;
    
    private void Awake()
    {
        _player = FindObjectOfType<BBPlayer>();
    }

    private void Update()
    {
        if (_didActivate || Vector3.Distance(_player.transform.position, transform.position) > activationDist)
        {
            interactGroup.alpha = Mathf.MoveTowards(interactGroup.alpha, 0, Time.deltaTime);
            return;
        }
        
        interactGroup.alpha = Mathf.MoveTowards(interactGroup.alpha, 1, Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.E))
        {
            interactAudio.Play();
            _didActivate = true;
            BGMManager.instance.desiredClip = null;
            StartCoroutine(Routine());
        }
    }

    private IEnumerator Routine()
    {
        yield return new WaitForSecondsRealtime(1.5f);
        explodeEffect.Play();
        for (float t = 0; t < 1f; t += Time.unscaledDeltaTime)
        {
            whiteFade.alpha = t;
            Time.timeScale = 1 - t;
            yield return null;
        }

        whiteFade.alpha = 1;
        yield return new WaitForSecondsRealtime(1.5f);
        SceneManager.LoadScene("Eldenring_StartMenu");
        Time.timeScale = 1f;
    }
}
