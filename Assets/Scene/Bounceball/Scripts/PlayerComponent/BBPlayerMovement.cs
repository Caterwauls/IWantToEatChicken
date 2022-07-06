using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BBPlayerMovement : BBPlayerComponentBase
{
    public System.Action<RaycastHit> onBlockCollision;

    public float bounce = 5f;
    public float acceleration = 199f;
    public float velocityLostOnNormalJump = 2f;
    
    public float ballSpeed = 10f;
    public float wallReflection = 10f;
    public bool playerMoveOn = true;

    public System.Action onBounce;
    public GameObject normalEffectPrefab;
    public float jumpSpeed = 100f;


    public bool pauseRay = false;
    public bool skipNormalJump = false;

    private Vector3 _vec;
    private BBPlayerReverseTile _reverse;

    private bool _reverseOn => _reverse.reverseOn;

    protected override void Awake()
    {
        base.Awake();
        _reverse = GetComponent<BBPlayerReverseTile>();
    }

    private void FixedUpdate()
    {
        PlayerMove();
    }

    void PlayerMove()
    {
        float speed = Time.fixedDeltaTime * ballSpeed;
        if (playerMoveOn)
        {
            _vec = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
            if (_reverseOn)
            {
                _vec = -_vec;
            }
            _rb.MovePosition(transform.position + _vec * speed);
        }


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
        if (shouldCheckSide && Physics.Raycast(sideCheck, 0.35f, LayerMask.GetMask("Ground")) && !pauseRay)
        {
            //Vector3 newVelocity = _rb.velocity;
            //newVelocity.x = 0;
            //newVelocity += -sideCheck.direction * bounce;
            //_rb.velocity = newVelocity;
            _rb.MovePosition(transform.position + -sideCheck.direction * bounce * Time.fixedDeltaTime);

        }
        Ray bottomCheck = new Ray();
        RaycastHit hit;
        Ray bottomSide0 = new Ray();
        Ray bottomSide1 = new Ray();

        Vector3 bottomSidePos0 = transform.position + Vector3.down * transform.lossyScale.y / 2f + Vector3.left * transform.lossyScale.x / 2f;
        bottomSide0.origin = bottomSidePos0 + Vector3.up * 0.1f;
        bottomSide0.direction = Vector3.down;

        Vector3 bottomSidePos1 = transform.position + Vector3.down * transform.lossyScale.y / 2f + Vector3.right * transform.lossyScale.x / 2f;
        bottomSide1.origin = bottomSidePos1 + Vector3.up * 0.1f;
        bottomSide0.direction = Vector3.down;

        Vector3 bottomPos = transform.position + Vector3.down * transform.lossyScale.y / 2f;
        bottomCheck.origin = bottomPos + Vector3.up * 0.1f;
        bottomCheck.direction = Vector3.down;

        if (_reverseOn)
        {
            bottomPos = transform.position + Vector3.up * transform.lossyScale.y / 2f;
            bottomCheck.origin = bottomPos + Vector3.down * 0.1f;
            bottomCheck.direction = Vector3.up;

            bottomSidePos0 = transform.position + Vector3.up * transform.lossyScale.y / 2f + Vector3.left * transform.lossyScale.x / 2f;
            bottomSide0.origin = bottomSidePos0 + Vector3.down * 0.1f;
            bottomSide0.direction = Vector3.up;

            bottomSidePos1 = transform.position + Vector3.up * transform.lossyScale.y / 2f + Vector3.right * transform.lossyScale.x / 2f;
            bottomSide1.origin = bottomSidePos0 + Vector3.down * 0.1f;
            bottomSide1.direction = Vector3.up;
        }


        // 아래쪽으로 체크
        if (Physics.Raycast(bottomCheck, out hit, 0.5f, LayerMask.GetMask("Ground")) ||
            Physics.Raycast(bottomSide0, out hit, 0.5f, LayerMask.GetMask("Ground")) ||
            Physics.Raycast(bottomSide1, out hit, 0.5f, LayerMask.GetMask("Ground"))
            && !pauseRay)
        {
            onBlockCollision?.Invoke(hit);

            if (skipNormalJump)
            {
                skipNormalJump = false;
                return;
            }

            //기본 점프
            Instantiate(normalEffectPrefab, bottomPos, Quaternion.identity);
            onBounce?.Invoke();
            Vector3 newVelocity = _rb.velocity;
            newVelocity.y = jumpSpeed;
            newVelocity.x = Mathf.MoveTowards(newVelocity.x, 0, velocityLostOnNormalJump);
            _rb.velocity = newVelocity;

            if (_reverseOn)
            {
                newVelocity.y = -jumpSpeed;
                _rb.velocity = newVelocity;
            }
        }
        Debug.DrawRay(bottomPos + Vector3.up * 0.1f, Vector3.down * 0.5f, Color.red);
        Debug.DrawRay(bottomSidePos0 + Vector3.up * 0.1f, Vector3.down * 0.5f, Color.blue);
        Debug.DrawRay(bottomSidePos1 + Vector3.up * 0.1f, Vector3.down * 0.5f, Color.blue);
    }
}
