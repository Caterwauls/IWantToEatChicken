using System.Collections;
using System.Collections.Generic;
using UnityEngine;



// 여기서쓴 온트리거는 큐브에 달려있는 구형 빈 게임 오브젝트에 충돌감지로 
// 다른 큐브를 감지하고 그 큐브와 자신의 크기를 비교하여 도망가거나 쫒아가거나를 결정.

public class CubeBehavior : MonoBehaviour
{

    public Cube myCube;
    public Rigidbody cubeRigidbody;
    public GameObject enemyGameObject;
    public float smoothTime = 0.1f;


    private Vector3 lastMovingVelocity;
    private Vector3 targetPosition;



    private void Start()
    {
        //StartCoroutine(CubeJump());
    }

    public void Chase(Cube enemy)
    {
        var dir = enemy.transform.position - transform.position;
        dir.y = 0;
        dir.Normalize();
        myCube.MoveMyVelocity(dir * myCube.cubeSpeed);
    }

    public void Run(Cube enemy)
    {
        Vector3 runDir = transform.position - enemy.transform.position;
        myCube.MoveMyVelocity(runDir.normalized * myCube.cubeSpeed);
    }

    private Vector3 _currentIdleVelocity;
    private float _currentIdleTime;
    private float _maxIdleTime;

    public void Idle()
    {
        _currentIdleTime += Time.fixedDeltaTime;

        if (_currentIdleTime > _maxIdleTime)
        {
            // _maxIdleTime 초마다 실행되는 코드.
            _currentIdleVelocity = Random.onUnitSphere;
            _currentIdleVelocity.y = 0f;
            _currentIdleVelocity = _currentIdleVelocity.normalized * myCube.cubeSpeed;
            _currentIdleTime = 0f;
            _maxIdleTime = Random.Range(0.5f, 1.5f);

        }
        myCube.MoveMyVelocity(_currentIdleVelocity);
    }

    public List<Cube> FindEnemiesInRange()
    {
        List<Cube> enemyCubes = new List<Cube>();
        Collider[] colliders = Physics.OverlapSphere(transform.position, 8f * transform.localScale.x);

        foreach (Collider col in colliders)
        {
            Cube cubeOfCol = col.GetComponentInParent<Cube>();
            if (cubeOfCol == myCube) continue;
            if (cubeOfCol == null) continue;
            enemyCubes.Add(cubeOfCol);
        }

        return enemyCubes;
    }

    Cube SearchClosestBiggerCube(List<Cube> AllenemiesInRange)
    {
        List<Cube> BiggerCubeList = new List<Cube>();

        for (int i = 0; i < AllenemiesInRange.Count; i++)
        {
            if (AllenemiesInRange[i] == null) continue;

            if (AllenemiesInRange[i].CanEat(myCube))
            {
                BiggerCubeList.Add(AllenemiesInRange[i]);
            }
        }
        Cube ClosestBiggerCube = GetClosetCubeInList(BiggerCubeList);

        return ClosestBiggerCube;
    }

    Cube SearchClosestSmallerCube(List<Cube> AllenemiesInRange)
    {
        List<Cube> SmallerCubeList = new List<Cube>();

        for (int i = 0; i < AllenemiesInRange.Count; i++)
        {
            if (AllenemiesInRange[i] == null) continue;

            if (myCube.CanEat(AllenemiesInRange[i]))
            {
                SmallerCubeList.Add(AllenemiesInRange[i]);
            }
        }
        Cube ClosestSmallerCube = GetClosetCubeInList(SmallerCubeList);

        return ClosestSmallerCube;

    }

    public Cube GetClosetCubeInList(List<Cube> cubeList)
    {
        float shortDis = Mathf.Infinity;
        float temp = 0;
        Cube closestCube = null;

        for (int i = 0; i < cubeList.Count; i++)
        {
            closestCube = cubeList[0];
            temp = Vector3.Distance(myCube.transform.position, cubeList[i].transform.position);
            if (temp < shortDis)
            {
                shortDis = temp;
                closestCube = cubeList[i];

            }

        }

        return closestCube;
    }


    //IEnumerator CubeJump()
    //{
    //    while (true)
    //    {
    //        float ranTime = Random.Range(1f, 5f);
    //        yield return new WaitForSeconds(ranTime);
    //        myCube.CubeJump();
    //    }

    //}

    private void FixedUpdate()
    {
        List<Cube> enemies = FindEnemiesInRange();
        Cube bigEnemy = SearchClosestBiggerCube(enemies);
        Cube smallEnemy = SearchClosestSmallerCube(enemies);

        if (bigEnemy != null)
        {
            Run(bigEnemy);
        }
        else if (smallEnemy != null)
        {
            Chase(smallEnemy);
        }
        else
        {
            Idle();
        }
    }

}
