using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BBFlyingPlayerControl : MonoBehaviour
{
    public GameObject flyingPlayer;
    public float skillAliveTime = 1.5f;
    public float skillDis = 100f;
    public GameObject prefab;

    private Vector3 _vec;
    private BBPlayer _player;

    private void Awake()
    {
        _player = FindObjectOfType<BBPlayer>();
        Vector3 startPosition = new Vector3(_player.transform.position.x, _player.transform.position.y, _player.transform.position.z - 5);
        transform.position = startPosition;
        flyingPlayer.transform.position = _player.transform.position;
        Physics.IgnoreCollision(_player.GetComponent<Collider>(), flyingPlayer.GetComponent<Collider>());
    }

    private void Start()
    {
        StartCoroutine(SkillRoutine());
    }

    private void FixedUpdate()
    {
        // Flying Player를 인풋대로 이동
        float speed = Time.fixedDeltaTime * _player.movement.ballSpeed * 2f;

        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");

        if (_player.reverse.reverseOn)
        {
            inputX = -inputX;
            inputY = -inputY;
        }

        _vec = new Vector3(inputX, inputY, 0);
        flyingPlayer.GetComponent<Rigidbody>().MovePosition(flyingPlayer.transform.position + _vec * speed);


    }

    private void Update()
    {
        if (Vector2.Distance(transform.position, flyingPlayer.transform.position) >= skillDis)
        {
            StopCoroutine(SkillRoutine());
            _player.transform.position = flyingPlayer.transform.position;
            _player.GetComponent<MeshRenderer>().enabled = true;
            _player.GetComponent<Rigidbody>().isKinematic = false;
            _player.movement.playerMoveOn = true;
            var flySkillStopEffect = Instantiate(prefab);
            flySkillStopEffect.transform.position = flyingPlayer.transform.position;
            Destroy(gameObject);
        }
    }


    IEnumerator SkillRoutine()
    {
        yield return new WaitForSecondsRealtime(skillAliveTime);

        _player.transform.position = flyingPlayer.transform.position;
        _player.GetComponent<MeshRenderer>().enabled = true;
        _player.GetComponent<Rigidbody>().isKinematic = false;
        _player.movement.playerMoveOn = true;
        var flySkillStopEffect = Instantiate(prefab);
        flySkillStopEffect.transform.position = flyingPlayer.transform.position;
        Destroy(gameObject);
    }
}
