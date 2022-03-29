using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    public Transform target;
    public float smoothTime = 0.2f;
    private Vector3 lastMovingVelocity;
    private Vector3 targetPosition;
    public Camera Cam;
    public float height = 15.5f;
    public float dist = 10.5f;


    private void Awake()
    {
        Cam = GetComponent<Camera>();

    }

    private void Move()
    {
        targetPosition = target.transform.position;

        Vector3 smoothPosition = Vector3.SmoothDamp(transform.position, targetPosition - (Vector3.forward * height) + (Vector3.up * dist),
            ref lastMovingVelocity, smoothTime);
        transform.position = smoothPosition;
    }



    private void LateUpdate()
    {
        if (target != null)
        {
            Move();

        }
        //else
        //    GameManager.instance.RestartGame();
    }



    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
}
