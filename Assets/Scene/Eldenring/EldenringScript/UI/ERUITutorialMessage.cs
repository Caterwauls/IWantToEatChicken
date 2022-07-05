using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ERUITutorialMessage : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            ERUIManager.instance.PlayInteractSound();
            Time.timeScale = 1f;
            gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        Time.timeScale = 0f;
    }
}
