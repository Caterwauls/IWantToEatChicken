using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class BBGroundAlignHelper : MonoBehaviour
{
    public bool alignRotation = false;
    
    private void Update()
    {
        if (Application.isPlaying) return;
        var pos = transform.position;
        pos.x = Mathf.Round(pos.x / 2) * 2;
        pos.y = Mathf.Round(pos.y / 2) * 2;
        pos.z = 0;
        transform.position = pos;
        if (alignRotation)
        {
            if (Vector3.Angle(transform.right, Vector3.right) < 90)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 0, -180);
            }
        }
    }
}
