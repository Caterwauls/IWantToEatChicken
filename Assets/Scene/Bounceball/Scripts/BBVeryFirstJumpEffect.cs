using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BBVeryFirstJumpEffect : MonoBehaviour
{
    public Effect eff;
    private bool _didActivate;
    private BBPlayerMovement _move;
    
    void Start()
    {
        _move = FindObjectOfType<BBPlayerMovement>();
        _move.onBounce = () =>
        {
            if (_didActivate) return;
            _didActivate = true;
            transform.position = _move.transform.position;
            eff.Play();
        };
    }
}
