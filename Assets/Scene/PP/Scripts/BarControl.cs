using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarControl : MonoBehaviour
{
    public float minPos;
    public float maxPos;
    public float barSpeed = 1.5f;

    public Transform dLine;
    public Transform uLine;
    public Transform trakingThing;
    public OneBlockManager gameManager;

    private void Awake()
    {
        minPos = dLine.position.y + 1.7f;
        maxPos = uLine.position.y - 1.7f;
    }
    private void Start()
    {

    }
    private void Update()
    {

    }

    public void BarMovement()
    {
        transform.position = Vector3.MoveTowards(transform.position,
                new Vector3(transform.position.x, trakingThing.position.y, transform.position.z), barSpeed * Time.fixedDeltaTime);
        if (transform.position.y >= maxPos) transform.position =
                new Vector3(transform.position.x, maxPos, transform.position.z);
        else if (transform.position.y <= minPos) transform.position =
                new Vector3(transform.position.x, minPos, transform.position.z);

    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.tag == "Player")
        {
            GetComponent<Rigidbody>().isKinematic = false;
        }
        else
            GetComponent<Rigidbody>().isKinematic = true;
    }
}
