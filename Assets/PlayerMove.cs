using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float jumpForce = 600f;
    public bool jumpCheck = true;
    public PlayerSkill playerSkill;

    private BoxCollider boxCollider;
    private Rigidbody playerRigidbody;

    private Cube _cube;


    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        _cube = GetComponent<Cube>();
    }

    IEnumerator JumpDelay()
    {

        yield return new WaitForSeconds(2);
        jumpCheck = true;

    }

    // Update is called once per frame
    private void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Space)) && (jumpCheck))
        {
            jumpCheck = false;
            playerRigidbody.AddForce(Vector3.up * jumpForce);
            StartCoroutine(JumpDelay());
        }
    }

    void FixedUpdate()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputZ = Input.GetAxis("Vertical");

        Vector3 velocity = new Vector3(inputX, 0, inputZ);

        velocity *= _cube.cubeSpeed;

        _cube.MoveMyVelocity(velocity);
    }
}
