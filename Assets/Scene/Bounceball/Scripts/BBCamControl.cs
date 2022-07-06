using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.Rendering;
using Cinemachine;

public class BBCamControl : MonoBehaviour
{
    public GameObject defaultCam;
    public GameObject reverseCam;
    public GameObject reverseEffect;
    public GameObject wideCam;

    private PostProcessVolume _reverseVol;
    private Vector3 _wideCamRot;
    private BBPlayer _player;

    private void Start()
    {
        _player = FindObjectOfType<BBPlayer>();
        _reverseVol = reverseEffect.GetComponent<PostProcessVolume>();

        _wideCamRot = wideCam.transform.rotation.eulerAngles;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (wideCam.activeSelf)
            {
                wideCam.SetActive(false);
            }
            else 
            {
                wideCam.SetActive(true);
            }
        }

        if (_player.reverse.reverseOn)
        {
            reverseCam.SetActive(true);
            defaultCam.SetActive(false);
            _wideCamRot.z = 180;
            wideCam.transform.eulerAngles = _wideCamRot;
        }
        else if (!_player.reverse.reverseOn)
        {
            defaultCam.SetActive(true);
            reverseCam.SetActive(false);
            _wideCamRot.z = 0;
            wideCam.transform.eulerAngles = _wideCamRot;
        }

        _reverseVol.weight = Mathf.MoveTowards(_reverseVol.weight, _player.reverse.reverseOn ? 1f : 0f, Time.deltaTime);
    }
}
