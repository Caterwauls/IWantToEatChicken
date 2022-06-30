﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ExecuteAlways]
public class BBDialogBox : MonoBehaviour
{
    public Transform guide;
    public float text_Xpos;
    public float text_Ypos;

    private Camera _mainCam;

    
    private void Awake()
    {
        _mainCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Camera.main.WorldToScreenPoint(guide.position + new Vector3(-text_Xpos, text_Ypos));
        if(transform.GetComponent<RectTransform>().anchoredPosition.x < -280)
        {
            Vector3 newPos = transform.GetComponent<RectTransform>().anchoredPosition;
            var newPosX = -270 - newPos.x;
            newPos.x = -280 + newPosX;
            transform.GetComponent<RectTransform>().anchoredPosition = newPos;
        }
    }
}