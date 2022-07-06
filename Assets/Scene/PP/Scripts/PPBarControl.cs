using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PPBarControl : MonoBehaviour
{
    public float minPos;
    public float maxPos;
    public float barSpeed = 1.5f;

    public Transform dLine;
    public Transform uLine;

    public Transform manualTrack;

    public Transform standardPos;

    private PPPlayer _player;

    private void Awake()
    {
        minPos = dLine.position.y + 1.7f;
        maxPos = uLine.position.y - 1.7f;
        _player = FindObjectOfType<PPPlayer>();
    }

    private void Update()
    {
        if (manualTrack == null && _player.restraintCount == 0) return;
        BarMovement();
    }

    public void BarMovement()
    {
        var trackPos = manualTrack != null ? manualTrack.transform.position : _player.transform.position;
        if (standardPos.position.x <= trackPos.x && transform.position.x < trackPos.x) return;
        else if (standardPos.position.x > trackPos.x && transform.position.x > trackPos.x) return;
        transform.position = Vector3.MoveTowards(
            transform.position,
            new Vector3(
                transform.position.x,
                trackPos.y,
                transform.position.z
                ),
            barSpeed * Time.fixedDeltaTime);


        if (transform.position.y >= maxPos)
            transform.position = new Vector3(transform.position.x, maxPos, transform.position.z);
        else if (transform.position.y <= minPos)
            transform.position = new Vector3(transform.position.x, minPos, transform.position.z);

    }
}
