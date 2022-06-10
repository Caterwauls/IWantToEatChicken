using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SignalManager : MonoBehaviour
{
    public AudioSource bgm;
    



    public void GoNextScene()
    {
        SceneManager.LoadScene(1);
        
    }

    public void bgmOff()
    {
        StartCoroutine(volumeDown());
    }

    IEnumerator volumeDown()
    {
        while (true)
        {
            if (bgm.volume <= 0) break;

            bgm.volume -= 0.5f * Time.deltaTime;
            yield return null;

        }
        yield break;
    }
}
