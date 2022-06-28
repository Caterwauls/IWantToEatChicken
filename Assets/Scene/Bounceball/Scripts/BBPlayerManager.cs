using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BBPlayerManager : MonoBehaviour
{
    public float bounce = 5f;
    public float acceleration = 199f;
    public float additionalGravity = -10f;
    public float ballSpeed = 10f;
    public float wallReflection = 10f;
    public bool playerMoveOn = true;


    public float jumpSpeed = 100f;
    public GameObject effectPrefab;

    private Vector3 _vec;
    private Rigidbody _rb;

    private void Awake()
    {
        _rb = transform.GetComponent<Rigidbody>();
    }



    // Update is called once per frame
    void FixedUpdate()
    {
        float speed = Time.fixedDeltaTime * ballSpeed;
        if (playerMoveOn)
        {
            _vec = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
            _rb.MovePosition(transform.position + _vec * speed);
        }
        


        _rb.velocity += Vector3.down * additionalGravity * Time.fixedDeltaTime;

        // 사이드로 체크
        Ray sideCheck = new Ray();
        bool shouldCheckSide = true;

        if (_vec.x < 0)
        {
            // 왼쪽
            sideCheck.origin = transform.position + Vector3.left * (transform.lossyScale.x / 2f - 0.1f);
            sideCheck.direction = Vector3.left;
            Debug.DrawRay(transform.position + Vector3.left * (transform.lossyScale.x / 2f - 0.1f), Vector3.left, Color.green);
        }
        else if (_vec.x > 0)
        {
            // 오른쪽
            sideCheck.origin = transform.position + Vector3.right * (transform.lossyScale.x / 2f - 0.1f);
            sideCheck.direction = Vector3.right;
            Debug.DrawRay(transform.position + Vector3.right * (transform.lossyScale.x / 2f - 0.1f), Vector3.right, Color.yellow);
        }
        else
        {
            shouldCheckSide = false;
        }

        // 만약 사이드로 튕겼을 경우
        if (shouldCheckSide && Physics.Raycast(sideCheck, 0.35f, LayerMask.GetMask("Ground")))
        {
            //Vector3 newVelocity = _rb.velocity;
            //newVelocity.x = 0;
            //newVelocity += -sideCheck.direction * bounce;
            //_rb.velocity = newVelocity;
            _rb.MovePosition(transform.position + -sideCheck.direction * bounce * Time.fixedDeltaTime);

        }

        Vector3 bottomPos = transform.position + Vector3.down * transform.lossyScale.y / 2f;

        // 아래쪽으로 체크
        if (Physics.Raycast(bottomPos + Vector3.up * 0.1f, Vector3.down, 0.5f, LayerMask.GetMask("Ground")))
        {
            Instantiate(effectPrefab, bottomPos, Quaternion.identity);
            Vector3 newVelocity = _rb.velocity;
            newVelocity.y = jumpSpeed;
            _rb.velocity = newVelocity;
        }
        Debug.DrawRay(bottomPos + Vector3.up * 0.1f, Vector3.down * 0.5f, Color.red);
        
        
    }
}
