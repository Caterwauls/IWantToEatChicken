using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneBlockManager : MonoBehaviour
{
    public Transform trakingThing;
    public float standardX;
    public BarControl L_Bar;
    public BarControl R_Bar;

    private float ballSpeed;

    private void Awake()
    {
        standardX = trakingThing.position.x;
        
    }
    private void Start()
    {

        if (trakingThing.CompareTag("Player"))
        {
            ballSpeed = trakingThing.GetComponent<PlayerControl>().PlayerSpeed;
        }
        else
        {
            ballSpeed = 3f;
            float sx = Random.Range(0, 2) == 0 ? -1 : 1;
            float sy = Random.Range(0, 2) == 0 ? -1 : 1;
            trakingThing.GetComponent<Rigidbody>().velocity = new Vector3(ballSpeed * sx, ballSpeed * sy, 0);
        }
    }

    private void Update()
    {
        if (trakingThing.position.x <= standardX)
        {
            L_Bar.BarMovement();
        }
        else
        {
            R_Bar.BarMovement();
        }

    }

}
