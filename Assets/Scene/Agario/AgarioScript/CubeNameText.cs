using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeNameText : MonoBehaviour
{
    public Cube target;

    void LateUpdate()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }
        transform.position = Camera.main.WorldToScreenPoint(target.transform.position + Vector3.up * (target.transform.lossyScale.y * 2));
    }
}
