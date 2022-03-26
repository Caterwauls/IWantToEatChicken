using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed = 10f;
    public float jumpForce = 600f;
    private BoxCollider boxCollider;
    private Rigidbody playerRigidbody;
    public bool jumpCheck = true;

    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
    }

    IEnumerator JumpDelay()
    {

        yield return new WaitForSeconds(2);
        jumpCheck = true;

    }

    // Update is called once per frame
    void Update()
    {

        float inputX = Input.GetAxis("Horizontal");

        float inputZ = Input.GetAxis("Vertical");

        if ((Input.GetKeyDown(KeyCode.Space)) && (jumpCheck))
        {
            jumpCheck = false;
            playerRigidbody.AddForce(Vector3.up * jumpForce);
            StartCoroutine(JumpDelay());
        }

        float fallSpeed = playerRigidbody.velocity.y;

        Vector3 velocity = new Vector3(inputX, 0, inputZ);

        velocity *= speed;
        velocity.y = fallSpeed;

        playerRigidbody.velocity = velocity;
    }
}
