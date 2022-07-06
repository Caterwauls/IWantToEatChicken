using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ContinueFromThisScene : MonoBehaviour
{
    private void Start()
    {
        Debug.Log($"Game Saved: {SceneManager.GetActiveScene().name}");
        PlayerPrefs.SetString("LastScene", SceneManager.GetActiveScene().name);
        PlayerPrefs.Save();
    }
}
