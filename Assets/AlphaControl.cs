using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlphaControl : MonoBehaviour
{
    public bool isPlaying;
    Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
        image.color = new Color(image.color.r, image.color.g, image.color.b, 0);
        isPlaying = false;
    }

    public void TextFadeEffect()
    {
        if (isPlaying) return;
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        isPlaying = true;

        while (image.color.a < 1f)
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, image.color.a + Time.deltaTime / 2f);
            yield return null;
        }

        yield return new WaitForSeconds(1f);


    }

}
