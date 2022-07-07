using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextAlpha : MonoBehaviour
{
    public bool isPlaying;
    Text text;

    private void Awake()
    {
        text = GetComponent<Text>();
        text.color = new Color(text.color.r, text.color.g, text.color.b, 0);
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

        while(text.color.a < 1f)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a + Time.deltaTime);
            yield return null;
        }

        yield return new WaitForSeconds(1f);

        yield return FadeOut();
    }

    IEnumerator FadeOut()
    {
        while(text.color.a > 0f)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a - Time.deltaTime);
            yield return null;
        }
    }






    


}
