using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgarioBGM : MonoBehaviour
{
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
}
