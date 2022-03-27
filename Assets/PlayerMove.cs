using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public bool jumpCheck = true;
    public Light particlesLight;

    private BoxCollider boxCollider;
    private Rigidbody playerRigidbody; 
    private PlayerSkill playerSkill;
    private Cube _cube;


    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        _cube = GetComponent<Cube>();
        playerSkill = GetComponent<PlayerSkill>();
        particlesLight = GetComponentInChildren<Light>();
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
            _cube.CubeJump();
            StartCoroutine(JumpDelay());
        }
        if ((Input.GetKeyDown(KeyCode.F)) && playerSkill._canUseTimeStop)
        {
            playerSkill.timeStop();

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
