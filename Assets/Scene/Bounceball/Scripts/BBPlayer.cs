using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BBPlayer : MonoBehaviour
{





    public BBPlayerReverseTile reverse { get; private set; }
    public BBPlayerMovement movement  { get; private set; }
    public BBPlayerAbility ability { get; private set; }



    private Rigidbody _rb;
    private float _timer = 0;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        reverse = GetComponent<BBPlayerReverseTile>();
        movement = GetComponent<BBPlayerMovement>();
        ability = GetComponent<BBPlayerAbility>();
    }


}
