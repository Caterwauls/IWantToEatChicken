using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public Transform target;
    public float smoothTime = 0.2f;
    public float height = 10f;
    public float dist = 5f;

    private Vector3 lastMovingVelocity;
    private Vector3 targetPosition;
    private void FixedUpdate()
    {
        _pCamControl();
    }
    private void _pCamControl()
    {
        float inputX = Input.GetAxis("Mouse X");
        float inputY = Input.GetAxis("Mouse Y");

        Vector2 mouseDelta = new Vector2(inputX, inputY);
        Vector3 camAngle = transform.rotation.eulerAngles;

        float camLook = camAngle.x - mouseDelta.y;

        if (camLook < 180f)
        {
            camLook = Mathf.Clamp(camLook, -1f, 70f);
        }
        else
            camLook = Mathf.Clamp(camLook, 337f, 361f);

        transform.rotation = Quaternion.Euler(camLook, camAngle.y + mouseDelta.x, camAngle.z);
    }

    private void LateUpdate()
    {
        if (target != null)
        {
            CamMove();
        }
    }
    private void CamMove()
    {
        targetPosition = target.transform.position;

        Vector3 smoothPosition = Vector3.SmoothDamp(transform.position,
            targetPosition - (Vector3.forward * height) + (Vector3.up * dist),
            ref lastMovingVelocity, smoothTime);
        transform.position = smoothPosition;
    }
}
