using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public struct PlayerState
    {
        public Vector3 position;
        public Vector3 velocity;
    }
    public bool canPlayerMove = false;
    public bool isCanResist = false;

    public float PlayerSpeed;
    public int resistNum = 0;

    private PlayerState[] _playerStates;
    private int _cursor = 0;
    private float i = 0;

    void Start()
    {

        Init();
        _playerStates = new PlayerState[10];
        StartCoroutine(SavePlayerState());
    }
    private void FixedUpdate()
    {
        if (!canPlayerMove) return;
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");

        Vector3 _vec = new Vector3(inputX, inputY, 0);
        GetComponent<Rigidbody>().velocity = _vec * Time.fixedDeltaTime * 100;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        LoadPlayerState();
    }

    void Init()
    {
        //transform.position = new Vector3(0.06f, 0.84f, 0);
        float sx = Random.Range(0, 4) == 0 ? -1 : 1;
        float sy = Random.Range(0, 4) == 0 ? -1 : 1;
        GetComponent<Rigidbody>().velocity = new Vector3(PlayerSpeed * sx, PlayerSpeed * sy, 0);
    }

    IEnumerator SavePlayerState()
    {

        while (true)
        {
            _playerStates[_cursor].position = transform.position;
            _playerStates[_cursor].velocity = GetComponent<Rigidbody>().velocity;
            _cursor++;
            yield return new WaitForSeconds(0.1f);
            if (_cursor == 10) _cursor = 0;
        }

    }

    public void LoadPlayerState()
    {
        if ((Input.GetKeyDown(KeyCode.Space)) && isCanResist)
        {
            transform.position = _playerStates[(_cursor + 1) % 10].position;
            GetComponent<Rigidbody>().velocity = _playerStates[(_cursor + 1) % 10].velocity;
            resistNum++;
        }
    }
}
