using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeAI : MonoBehaviour
{
    public Rigidbody rigidbody;
    public Cube cube;
    private void Start()
    {
        StartCoroutine(movement());

    }

    IEnumerator movement()
    {
        float dir1 = Random.Range(-1f, 1f);
        float dir2 = Random.Range(-1f, 1f);

        rigidbody.velocity = new Vector3(dir1, 0, dir2);
        yield break;
    }

}
