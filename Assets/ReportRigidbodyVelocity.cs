using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReportRigidbodyVelocity : MonoBehaviour
{
    private void FixedUpdate()
    {
        Debug.Log(GetComponent<Rigidbody>().velocity);
    }
}
