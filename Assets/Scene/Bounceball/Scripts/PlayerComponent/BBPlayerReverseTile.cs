using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BBPlayerReverseTile : BBPlayerComponentBase
{
    public bool reverseOn = false;
    public float playerMoveDuration = 1f;
    private BBPlayerMovement _movement;
    public GameObject reverseEffect;
    public GameObject rereverseEffect;


    protected override void Awake()
    {
        base.Awake();
        _movement = GetComponent<BBPlayerMovement>();
    }

    private void Start()
    {
        GetComponent<BBPlayerMovement>().onBlockCollision += OnCollision;
    }

    private void OnCollision(RaycastHit hit)
    {
        StartCoroutine(OnCollisionRoutine(hit));
    }

    IEnumerator OnCollisionRoutine(RaycastHit hit)
    {
        if (hit.collider.tag == "ReverseTile")
        {
            reverseOn = true;
            var destination = hit.collider.GetComponent<BBReverseTile>().reversePlayerPos.position;
            _player.movement.playerMoveOn = false;
            _player.GetComponent<Rigidbody>().velocity = Vector3.zero;
            _player.GetComponent<BoxCollider>().enabled = false;
            Instantiate(reverseEffect, transform.position, Quaternion.identity);

            yield return MovePlayerRoutine(_player.transform.position, destination, playerMoveDuration);

            _player.GetComponent<BoxCollider>().enabled = true;
            _player.movement.playerMoveOn = true;
        }

        if (hit.collider.tag == "RereverseTile")
        {
            if (!reverseOn) yield break;

            var destination = hit.collider.GetComponent<BBReReverseTile>().reReversePlayerPos.position;

            _movement.playerMoveOn = false;
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            GetComponent<BoxCollider>().enabled = false;
            reverseOn = false;
            Instantiate(rereverseEffect, transform.position, Quaternion.identity);

            yield return MovePlayerRoutine(_player.transform.position, destination, playerMoveDuration);

            _player.GetComponent<BoxCollider>().enabled = true;
            _player.movement.playerMoveOn = true;
        }
    }

    IEnumerator MovePlayerRoutine(Vector3 start, Vector3 end, float duration)
    {
        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            transform.position = Vector3.Lerp(start, end, t / duration);
            yield return null;
        }
        transform.position = end;
    }
}
