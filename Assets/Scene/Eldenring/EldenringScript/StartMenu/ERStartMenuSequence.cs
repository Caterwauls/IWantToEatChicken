using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ERStartMenuSequence : MonoBehaviour
{
    public CanvasGroup fadeGroup;
    public CanvasGroup pressAnyButtonGroup;
    public Flow flow;

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
            flow.StartFlow();
            while (true)
            {
                pressAnyButtonGroup.alpha = Mathf.Sin(elapsed * 2f + 3f/2f * Mathf.PI) / 2f + 0.5f;
                elapsed += Time.deltaTime;
                yield return null;
            }
        }
    }
}
