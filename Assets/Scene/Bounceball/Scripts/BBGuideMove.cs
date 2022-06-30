using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BBGuideMove : MonoBehaviour
{
    public Transform target;

    private void FixedUpdate()
    {
        Vector3 TargetPos = new Vector3(target.position.x - 1.4f, target.position.y + 2.1f, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, TargetPos, Time.deltaTime);
    }
}
