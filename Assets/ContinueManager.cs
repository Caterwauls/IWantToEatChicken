using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ContinueManager : MonoBehaviour
{
    private void Update()
    {
        if (PlayerPrefs.HasKey("LastScene"))
        {
            var sceneName = PlayerPrefs.GetString("LastScene");
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            SceneManager.LoadScene("PPScene");
        }
    }
}
