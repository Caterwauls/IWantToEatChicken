using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixCameraAspectRatio : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Camera>().aspect = 16f / 9f;    
    }

    
}
