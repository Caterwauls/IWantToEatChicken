using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    public Transform enemy;
    public Transform player;

    public float cameraSlack;
    public float cameraDistance;

    private Vector3 pivotPoint;

    private void Start()
    {
        pivotPoint = transform.position;
    }

    private void LateUpdate()
    {
        Vector3 current = pivotPoint;
        Vector3 target = player.transform.position + Vector3.up;
        pivotPoint = Vector3.MoveTowards(current, target, Vector3.Distance(current, target) * cameraSlack);

        transform.position = pivotPoint;
        transform.LookAt((enemy.position + player.position) / 2);
        transform.position -= transform.forward * cameraDistance;
    }


}
