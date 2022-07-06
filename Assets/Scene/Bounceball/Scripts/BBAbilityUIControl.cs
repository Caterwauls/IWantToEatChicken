using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BBAbilityUIControl : MonoBehaviour
{
    public int rotSpeed = -60;
    private BBPlayer _player;

    private float _time;

    private void Start()
    {
        _player = FindObjectOfType<BBPlayer>();
    }

    void Update()
    {
        transform.Rotate(0, 0, rotSpeed * Time.deltaTime);

        var img = GetComponent<RawImage>();
        Color color;
    
        if (_player.ability.dashAbilityOn) color = new Color(1, 0.5f, 0.9f); // 대쉬
        else if (_player.ability.flyAbilityOn) color = new Color(0.3f, 1, 0.2f); // 날기
        else if (_player.ability.doubleJumpOn) color = new Color(0.15f, 0.48f, 1); // 더블점프
        else
        {
            img.color = Color.clear;
            return;
        }
        
        if (_time < 0.5f)
        {
            color.a = 1 - _time;
        }
        else
        {
            color.a = _time;
            if (_time > 1f) _time = 0;
        }
        img.color = color;
        _time += Time.deltaTime;
    }

}
