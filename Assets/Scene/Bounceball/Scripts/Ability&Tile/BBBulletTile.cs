using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BBBulletTile : MonoBehaviour
{
    public bool tileDirRight;

    private void Start()
    {
        tileDirRight = Mathf.Abs(transform.rotation.eulerAngles.z - 180) < 30;
    }

    private void OnDrawGizmos()
    {
        var cachedTransform = transform;
        var pos = cachedTransform.position;
        var right = Vector3.right;
        var left = Vector3.left;
        var up = Vector3.up;
        var down = Vector3.down;
        
        Gizmos.color = Color.magenta;
        if (Mathf.Abs(transform.rotation.eulerAngles.z - 180) < 30)
        {
            Gizmos.DrawLine(pos, pos + right * 5);
            Gizmos.DrawLine(pos + right * 5, pos + right * 3 + up);
            Gizmos.DrawLine(pos + right * 5, pos + right * 3 + down);
        }
        else
        {
            Gizmos.DrawLine(pos, pos + left * 5);
            Gizmos.DrawLine(pos + left * 5, pos + left * 3 + up);
            Gizmos.DrawLine(pos + left * 5, pos + left * 3 + down);
        }
        
    }
}
