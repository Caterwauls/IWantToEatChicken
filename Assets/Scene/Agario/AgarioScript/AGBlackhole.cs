using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AGBlackhole : MonoBehaviour
{
    private void OnEnable()
    {
        var newPos = FindObjectOfType<PlayerMove>().transform.position;
        transform.position = new Vector3(newPos.x, transform.position.y, newPos.z);
    }
}
