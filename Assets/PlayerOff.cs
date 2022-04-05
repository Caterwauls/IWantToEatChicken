using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerOff : MonoBehaviour
{
    public UnityEvent playerOff;
    void Start()
    {
        playerOff.Invoke();
    }


}
