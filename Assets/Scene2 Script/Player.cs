using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public bool jumpCheck = true;
    public Transform camTransform;

    public Camera cameraPoint;
    public PlayerCamera playerCamera;
    public float distanceOfBeam = 3f;

    private Rigidbody _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        cameraPoint = Camera.main;

    }

    void Update()
    {
        ShootBeam();
        
        if ((Input.GetKeyDown(KeyCode.Space)) && (jumpCheck))
        {
            jumpCheck = false;
            CubeJump();
            StartCoroutine(JumpDelay());
        }
    }

    private void FixedUpdate()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputZ = Input.GetAxis("Vertical");

        Vector3 velocity = new Vector3(inputX, 0, inputZ);

        velocity = camTransform.rotation * velocity;
        velocity.y = 0;
        velocity = velocity.normalized;
        velocity *= 10f;

        MoveMyVelocity(velocity);

    }



    public void MoveMyVelocity(Vector3 targetVelocity)
    {

        Vector3 newVelocity = Vector3.MoveTowards(_rb.velocity, targetVelocity, 20f * Time.fixedDeltaTime);
        newVelocity.y = _rb.velocity.y;
        _rb.velocity = newVelocity;
    }

    public void CubeJump()
    {
        _rb.AddForce(Vector3.up.normalized * 10f, ForceMode.Impulse);
    }

    IEnumerator JumpDelay()
    {

        yield return new WaitForSeconds(2);
        jumpCheck = true;

    }

    void ShootBeam()
    {
        Vector3 rayOrigin = transform.position;
        Vector3 rayDir = cameraPoint.ScreenPointToRay(Input.mousePosition).direction;

        

        if (Input.GetMouseButtonDown(0))
        {
            Physics.Raycast(rayOrigin, rayDir, distanceOfBeam);
            Debug.DrawRay(rayOrigin, rayDir * distanceOfBeam, Color.green);
        }
    }
}
