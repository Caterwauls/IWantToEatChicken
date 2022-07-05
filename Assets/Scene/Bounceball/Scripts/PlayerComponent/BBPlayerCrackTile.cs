using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BBPlayerCrackTile : BBPlayerComponentBase
{
    public Material crackedTile;
    public GameObject crackEffect;
    //public GameObject crackSound;

    private BBPlayerReverseTile _reverse;
    private BBPlayerMovement _movement;

    protected override void Awake()
    {
        base.Awake();
        _reverse = GetComponent<BBPlayerReverseTile>();
        _movement = GetComponent<BBPlayerMovement>();
    }

    private void Start()
    {
        GetComponent<BBPlayerMovement>().onBlockCollision += OnCollision;
    }

    private void OnCollision(RaycastHit hit)
    {
        if (hit.collider.tag != "CrackTile") return;

        if(hit.collider.GetComponent<BBCrackTile>().hitCount == 1)
        {
            var effect = Instantiate(crackEffect);
            effect.transform.position = hit.collider.transform.position;
            Destroy(hit.collider.transform.gameObject);
        }
        hit.collider.GetComponent<MeshRenderer>().material = crackedTile;
        //Instantiate(crackedTile, hit.transform.position, Quaternion.identity);
        hit.collider.GetComponent<BBCrackTile>().hitCount++;
        
    }
}
