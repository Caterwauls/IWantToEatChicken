using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.Events;
using Cinemachine;
using Plane = UnityEngine.Plane;
using Vector3 = UnityEngine.Vector3;

public class PlayerMove : MonoBehaviour
{
    //public bool jumpCheck = true;
    public Light particlesLight;
    public CinemachineVirtualCamera playerCamera;
    public Camera mainCam;
    public string playerName = null;
    public float distanceOfMaxSpeed = 8f;


    private BoxCollider boxCollider;
    private Rigidbody playerRigidbody;
    private PlayerSkill playerSkill;
    private Cube _cube;
    private bool _isCanPlayerMove;


    private void Awake()
    {
        
        playerRigidbody = GetComponent<Rigidbody>();
        _cube = GetComponent<Cube>();
        playerSkill = GetComponent<PlayerSkill>();
        particlesLight = GetComponentInChildren<Light>();
        playerName = PlayerPrefs.GetString("AgarioPlayerName");
        _cube.cubeName = playerName;

    }

    //IEnumerator JumpDelay()
    //{

    //    yield return new WaitForSeconds(2);
    //    jumpCheck = true;

    //}

    private void Start()
    {
        _isCanPlayerMove = AGGameManager.instance.flow.isCanPlayerMove;
    }

    private void Update()
    {
        _isCanPlayerMove = AGGameManager.instance.flow.isCanPlayerMove;
        if (!_isCanPlayerMove) return;
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
        if (!_isCanPlayerMove) return;
        var ray = mainCam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 1000f))
        {
            var dir = hit.point - transform.position;
            dir.y = 0;
            if (dir.magnitude > distanceOfMaxSpeed) 
                dir = dir.normalized * distanceOfMaxSpeed;

            dir /= 8;
            _cube.MoveMyVelocity(dir * _cube.cubeSpeed);
        }
    }
}
