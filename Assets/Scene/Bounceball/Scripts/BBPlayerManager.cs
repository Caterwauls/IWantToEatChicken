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
    public GameObject normalEffectPrefab;
    public GameObject jumpTileEffectPrefab;

    public float teleportAbilityDis = 10f;
    public float teleportAbilityDelay = 5;
    public bool teleportAbilityOn = false;


    private Vector3 _vec;
    private Rigidbody _rb;
    private float _timer = 0;

    private void Awake()
    {
        _rb = transform.GetComponent<Rigidbody>();
    }



    // Update is called once per frame
    void FixedUpdate()
    {
        if (teleportAbilityOn)
        {
            UseTeleportAbility();

        }

        PlayerMove();


    }

    void PlayerMove()
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
        Ray bottomCheck = new Ray();
        RaycastHit hit;
        Vector3 bottomPos = transform.position + Vector3.down * transform.lossyScale.y / 2f;
        bottomCheck.origin = bottomPos + Vector3.up * 0.1f;
        bottomCheck.direction = Vector3.down;

        // 아래쪽으로 체크
        if (Physics.Raycast(bottomCheck, out hit, 0.5f, LayerMask.GetMask("Ground")))
        {
            //점프 타일일때
            if (hit.collider.tag == "JumpTile")
            {
                Instantiate(normalEffectPrefab, bottomPos, Quaternion.identity);
                Instantiate(jumpTileEffectPrefab, hit.transform.position, Quaternion.identity);
                Vector3 jumpTileVelocity = _rb.velocity;
                jumpTileVelocity.y = jumpSpeed * 2;
                _rb.velocity = jumpTileVelocity;
                return;
            }

            //기본 점프
            Instantiate(normalEffectPrefab, bottomPos, Quaternion.identity);
            Vector3 newVelocity = _rb.velocity;
            newVelocity.y = jumpSpeed;
            _rb.velocity = newVelocity;
        }
        Debug.DrawRay(bottomPos + Vector3.up * 0.1f, Vector3.down * 0.5f, Color.red);
    }
    void UseTeleportAbility()
    {


        if (Input.GetKeyDown(KeyCode.F))
        {
            if (_vec.x < 0)
            {
                // 왼쪽
                transform.position = transform.position + Vector3.left * teleportAbilityDis;
                teleportAbilityOn = false;


            }
            else if (_vec.x >= 0)
            {
                // 오른쪽
                transform.position = transform.position + Vector3.right * teleportAbilityDis;
                teleportAbilityOn = false;
            }
        }

        _timer += Time.fixedDeltaTime;

        if (_timer >= teleportAbilityDelay)
        {
            teleportAbilityOn = false;
            _timer = 0;
        }

    }
}
