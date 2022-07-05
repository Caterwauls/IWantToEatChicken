using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ERStartMenuSequence : MonoBehaviour
{
    public CanvasGroup fadeGroup;
    public CanvasGroup pressAnyButtonGroup;
    public AudioSource interactAudio;
    public string nextSceneName;

    private void Start()
    {
        StartCoroutine(Routine());
        IEnumerator Routine()
        {
            fadeGroup.alpha = 1f;
            pressAnyButtonGroup.alpha = 0f;
            for (float t = 0; t < 1; t += Time.deltaTime / 2f)
            {
                fadeGroup.alpha = 1f - t;
                yield return null;
            }
            fadeGroup.alpha = 0f;
            yield return new WaitForSeconds(2f);
            var elapsed = 0f;
            while (true)
            {
                pressAnyButtonGroup.alpha = Mathf.Sin(elapsed * 2f + 3f/2f * Mathf.PI) / 2f + 0.5f;
                elapsed += Time.deltaTime;
                if (Input.GetKeyDown(KeyCode.Space)) break;
                yield return null;
            }

            interactAudio.Play();
            BGMManager.instance.desiredClip = null;
            for (float t = 0; t < 1; t += Time.deltaTime * 2f)
            {
                pressAnyButtonGroup.alpha = 1 - t;
                yield return null;
            }
            for (float t = 0; t < 1; t += Time.deltaTime / 2f)
            {
                fadeGroup.alpha = t;
                yield return null;
            }

            fadeGroup.alpha = 1;
            yield return new WaitForSeconds(1f);
            SceneManager.LoadScene(nextSceneName);
        }
    }
}
