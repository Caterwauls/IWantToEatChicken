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
        transform.position = Vector3.MoveTowards(
            transform.position, 
            new Vector3(
                transform.position.x, 
                manualTrack != null ? manualTrack.transform.position.y : _player.transform.position.y, 
                transform.position.z
                ), 
            barSpeed * Time.fixedDeltaTime);
        
        if (transform.position.y >= maxPos) 
            transform.position = new Vector3(transform.position.x, maxPos, transform.position.z);
        else if (transform.position.y <= minPos) 
            transform.position = new Vector3(transform.position.x, minPos, transform.position.z);

    }
}
