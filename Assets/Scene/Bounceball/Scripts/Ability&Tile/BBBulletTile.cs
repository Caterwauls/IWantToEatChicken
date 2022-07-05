using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BBBulletTile : MonoBehaviour
{
    public bool tileDirRight;

    private void Start()
    {
        if(Mathf.Abs(transform.rotation.eulerAngles.z - 180) < 30)
        {
            tileDirRight = true;
        }
        else
        {
            tileDirRight = false;
        }
    }
}
