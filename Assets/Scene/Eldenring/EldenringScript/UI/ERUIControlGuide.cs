using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ERUIControlGuide : MonoBehaviour
{
    public bool isOpen = true;
    public GameObject[] objects;
    public AudioSource audio;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            isOpen = !isOpen;
            audio.Play();
            foreach (var obj in objects)
            {
                obj.SetActive(isOpen);
            }
        }
    }
}
