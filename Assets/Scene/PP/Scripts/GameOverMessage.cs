using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverMessage : MonoBehaviour
{
    private void OnEnable()
    {
        StartCoroutine(EndMessage());
    }

    IEnumerator EndMessage()
    {
        float myAlpha = transform.GetComponentInChildren<Text>().color.a;
        if (myAlpha < 1)
        {
            myAlpha += 0.0005f;
            yield return new WaitForSecondsRealtime(0.01f);
        }

        yield return new WaitForSecondsRealtime(5f);

        if (myAlpha >= 1)
        {
            myAlpha -= 0.0005f;
            yield return new WaitForSecondsRealtime(0.01f);

        }
        else if (myAlpha <= 0)
        {
            transform.gameObject.SetActive(false);
        }


    }
}
