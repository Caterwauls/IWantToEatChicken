using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchWhereIsEnemey : MonoBehaviour
{
    private static Collider[] _collisionBuffer = new Collider[128];

    public Cube player;
    public float rotSpeed = 360f;


    private void Update()
    {
        float shortestDis = float.PositiveInfinity;
        Cube shortestEnemy = null;
        transform.position = player.transform.position;

        var numOfEntity = Physics.OverlapSphereNonAlloc(transform.position, 100, _collisionBuffer);

        for(int i = 0; i < numOfEntity; i++)
        {
            var col = _collisionBuffer[i];
            var cube = col.GetComponent<CubeBehavior>();
            if (cube == null) continue;
            var sqrDist = Vector3.SqrMagnitude(cube.transform.position - player.transform.position);
            if (sqrDist > shortestDis) continue;
            if (!player.CanEat(cube.myCube)) continue;
            shortestDis = sqrDist;
            shortestEnemy = cube.myCube;
            
        }

        if (shortestEnemy == null) return;
        ThereIsMyEnemy(shortestEnemy.transform);
    }

    void ThereIsMyEnemy(Transform target)
    {
        var dir = target.position - transform.position;
        dir.Normalize();
        var dirRot = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, dirRot, rotSpeed * Time.deltaTime);
    }
}
