using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchWhereIsEnemey : MonoBehaviour
{
    private static Collider[] _collisionBuffer = new Collider[128];

    public GameObject player;
    public float rotSpeed = 360f;
    public float shortestDis = float.PositiveInfinity;
    public Cube shortestEnemy = null;

    private void Update()
    {
        transform.position = player.transform.position;

        var numOfEntity = Physics.OverlapSphereNonAlloc(transform.position, 100, _collisionBuffer);
        List<Cube> enemyCubes = new List<Cube>();
        for(int i = 0; i < numOfEntity; i++)
        {
            var col = _collisionBuffer[i];
            if (col.GetComponent<CubeBehavior>() != null)
            {
                enemyCubes.Insert(0, col.GetComponent<Cube>());
            }
            else continue;
        }
        for(int i = 0; i < enemyCubes.Count; i++)
        {
            if(shortestDis >= Vector3.SqrMagnitude(enemyCubes[i].transform.position - player.transform.position) && player.GetComponent<Cube>().CanEat(enemyCubes[i]))
            {
                shortestDis = Vector3.SqrMagnitude(enemyCubes[i].transform.position - player.transform.position);
                shortestEnemy = enemyCubes[i];
            }
        }
        if (shortestEnemy == null) return;
        ThereIsMyEnemy(shortestEnemy.transform);
        enemyCubes.Clear();
    }

    void ThereIsMyEnemy(Transform target)
    {
        var dir = target.position - transform.position;
        dir.Normalize();
        var dirRot = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, dirRot, rotSpeed * Time.deltaTime);
    }
}
