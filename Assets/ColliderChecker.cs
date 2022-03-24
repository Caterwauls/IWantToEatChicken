using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderChecker : MonoBehaviour
{
    public bool isTriggerOn = false;
    public Cube cube;
    protected void OnTriggerStay(Collider ohterCube)
    {
        if (ohterCube.tag == "Cube")
        {
            isTriggerOn = true;
            gameObject.SendMessageUpwards("OnTriggerStay", ohterCube);
            this.transform.localScale = Vector3.one * Mathf.Pow(cube.energy, 1 / 3f);
        }

    }
}
