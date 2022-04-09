using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCubeAI : MonoBehaviour
{
    public Transform player;
    public Transform eye;

    private void Awake()
    {
        
    }
    void Start()
    {

    }

    void Update()
    {
        lookTarget();



    }

    private void lookTarget()
    {
        Quaternion lookAt = Quaternion.identity;
        Vector3 lookatVec = (player.position - eye.position).normalized;
        lookAt.SetLookRotation(lookatVec);
        eye.rotation = lookAt;
    }
}
