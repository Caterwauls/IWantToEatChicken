using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ERUILockedOnCursor : MonoBehaviour
{
    public ERPlayerCamera playerCam;
    
    private CanvasGroup _canvasGroup;
    private Camera _mainCam;
    
    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _mainCam = Camera.main;
    }

    private void Update()
    {
        if (playerCam.lockOnTarget == null)
        {
            _canvasGroup.alpha = 0;
            return;
        }
        _canvasGroup.alpha = 1;
        transform.position = _mainCam.WorldToScreenPoint(playerCam.lockOnTarget.lockOnPosition.position);
    }
}
