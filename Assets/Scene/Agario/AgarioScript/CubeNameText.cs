using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeNameText : MonoBehaviour
{
    public Cube target;

    void LateUpdate()
    {
        
        transform.position = Camera.main.WorldToScreenPoint(target.transform.position + Vector3.up * (target.transform.lossyScale.y * 2));
    }
    private void Update()
    {
        if (target == null || target.gameObject.activeSelf == false)
        {
            Destroy(gameObject);
        }
    }
}
