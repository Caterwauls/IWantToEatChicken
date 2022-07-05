using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BBAbilityUIControl : MonoBehaviour
{
    public int rotSpeed = -60;
    public BBPlayer player;

    private float _time;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {


        transform.Rotate(0, 0, rotSpeed * Time.deltaTime);

        //대쉬어빌리티
        if (player.ability.dashAbilityOn)
        {
            if (_time < 0.5f)
            {
                GetComponent<RawImage>().color = new Color(1, 0.5f, 0.9f, 1 - _time);

            }
            else
            {
                GetComponent<RawImage>().color = new Color(1, 0.5f, 0.9f, _time);
                if (_time > 1f)
                {
                    _time = 0;
                }
            }
            _time += Time.deltaTime;
        }

        //플라이어빌리티
        else if (player.ability.flyAbilityOn)
        {
            if (_time < 0.5f)
            {
                GetComponent<RawImage>().color = new Color(0.3f, 1, 0.2f, 1 - _time);

            }
            else
            {
                GetComponent<RawImage>().color = new Color(0.3f, 1, 0.2f, _time);
                if (_time > 1f)
                {
                    _time = 0;
                }
            }
            _time += Time.deltaTime;
        }

        //더블점프
        else if (player.ability.doubleJumpOn)
        {
            if (_time < 0.5f)
            {
                GetComponent<RawImage>().color = new Color(0.15f, 0.48f,1, 1 - _time);

            }
            else
            {
                GetComponent<RawImage>().color = new Color(0.15f, 0.48f, 1, _time);
                if (_time > 1f)
                {
                    _time = 0;
                }
            }
            _time += Time.deltaTime;
        }

        //노말상태
        else
        {
            if (_time < 0.5f)
            {
                GetComponent<RawImage>().color = new Color(1, 1, 1, 1 - _time);

            }
            else
            {
                GetComponent<RawImage>().color = new Color(1, 1, 1, _time);
                if (_time > 1f)
                {
                    _time = 0;
                }
            }
            _time += Time.deltaTime;
        }

    }

}
