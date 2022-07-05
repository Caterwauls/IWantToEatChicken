using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.Rendering;
using Cinemachine;

public class BBCamControl : MonoBehaviour
{
    public BBPlayer player;
    public GameObject defaultCam;
    public GameObject reverseCam;
    public GameObject reverseEffect;
    public GameObject wideCam;

    private PostProcessVolume _v;
    private ColorGrading _cg;
    private ChromaticAberration _ca;
    private Vector3 _wideCamRot;

    private void Start()
    {
        _v = reverseEffect.GetComponent<PostProcessVolume>();
        _v.profile.TryGetSettings(out _cg);
        _v.profile.TryGetSettings(out _ca);

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

        if (player.reverse.reverseOn)
        {
            reverseCam.SetActive(true);
            defaultCam.SetActive(false);
            reverseEffect.SetActive(true);
            _wideCamRot.z = 180;
            wideCam.transform.eulerAngles = _wideCamRot;
        }
        else if (!player.reverse.reverseOn)
        {
            defaultCam.SetActive(true);
            reverseCam.SetActive(false);
            reverseEffect.SetActive(false);
            _wideCamRot.z = 0;
            wideCam.transform.eulerAngles = _wideCamRot;
        }

        if (reverseEffect.activeSelf)
        {
            if(_cg.hueShift.value < 180)
            {
                _cg.hueShift.value += 180 * 2 * Time.deltaTime;
            }
            if (_ca.intensity.value < 0.5f)
            {
                _ca.intensity.value += Time.deltaTime;
            }
        }
    }
}
