using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour
{
    private Quaternion myRotation;
    private Quaternion newRotation;
    private Quaternion plusRotation;

    public Vector3 OrbitVector;

    private void Awake()
    {
        plusRotation = Quaternion.Euler(OrbitVector * Time.deltaTime);  
    }
    void Update()
    {
        myRotation = transform.rotation;
        newRotation = myRotation * plusRotation;
        transform.rotation = newRotation;
    }
}
