using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;
public class PPGameManager : MonoBehaviour
{
    public Flow flow;
    public GameObject cinemachineDirector;
    public GameObject resistMessage;
    public GameObject endMessage;
    public GameObject restartMessage;
    public GameObject playerParticle;

    private void Awake()
    {
        cinemachineDirector.SetActive(false);
        resistMessage.SetActive(false);
        endMessage.SetActive(false);
        restartMessage.SetActive(false);
        playerParticle.SetActive(false);
    }
    private void Start()
    {
        flow.StartFlow();
    }

    public void restartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }
}
