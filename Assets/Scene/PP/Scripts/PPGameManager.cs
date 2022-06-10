using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using UnityEngine.Rendering.PostProcessing;
public class PPGameManager : MonoBehaviour
{
    public Flow flow;

    private void Start()
    {
        flow.StartFlow();
    }
}
