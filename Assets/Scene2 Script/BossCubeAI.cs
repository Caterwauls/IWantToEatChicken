using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCubeAI : MonoBehaviour
{
    public Transform player;
    void Start()
    {

    }

    void Update()
    {
        transform.LookAt(player);
    }
}
