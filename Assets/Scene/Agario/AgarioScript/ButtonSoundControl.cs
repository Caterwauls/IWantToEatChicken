using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonSoundControl : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private AudioSource _audio;

    private void Awake()
    {
        _audio = GetComponent<AudioSource>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _audio.Play();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
    }
}
