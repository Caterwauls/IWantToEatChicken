using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityUIControl : MonoBehaviour
{
    public int rotSpeed = -60;
    public BBPlayerManager player;

    private float _time;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {


        transform.Rotate(0, 0, rotSpeed * Time.deltaTime);

        if (player.teleportAbilityOn)
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
        else if (!player.teleportAbilityOn)
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
