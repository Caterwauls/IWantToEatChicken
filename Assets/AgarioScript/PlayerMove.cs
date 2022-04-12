using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Cinemachine;

public class PlayerMove : MonoBehaviour
{
    //public bool jumpCheck = true;
    public Light particlesLight;
    public CinemachineVirtualCamera playerCamera;
    public Camera mainCam;


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

    private void Start()
    {


    }

    //IEnumerator JumpDelay()
    //{

    //    yield return new WaitForSeconds(2);
    //    jumpCheck = true;

    //}


    private void Update()
    {

        //if ((Input.GetKeyDown(KeyCode.Space)) && (jumpCheck))
        //{
        //    jumpCheck = false;
        //    _cube.CubeJump();
        //    StartCoroutine(JumpDelay());
        //}
        if ((Input.GetKeyDown(KeyCode.F)) && playerSkill._canUseTimeStop)
        {
            playerSkill.timeStop();

        }
        playerCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.y = transform.localScale.y * 15;

    }



    void FixedUpdate()
    {
        Vector3 rayDirection = mainCam.ScreenPointToRay(Input.mousePosition).direction;

        //float inputX = Input.GetAxis("Horizontal");
        //float inputZ = Input.GetAxis("Vertical");

        float inputX = rayDirection.x;
        float inputZ = rayDirection.z;

        Vector3 velocity = new Vector3(inputX, 0, inputZ);

        velocity *= _cube.cubeSpeed;

        _cube.MoveMyVelocity(velocity);

    }



}
