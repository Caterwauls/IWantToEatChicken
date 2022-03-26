using System.Collections;
using System.Collections.Generic;
using UnityEngine;



// 여기서쓴 온트리거는 큐브에 달려있는 구형 빈 게임 오브젝트에 충돌감지로 
// 다른 큐브를 감지하고 그 큐브와 자신의 크기를 비교하여 도망가거나 쫒아가거나를 결정.

public class CubeBehavior : MonoBehaviour
{
    
    public Cube cube;
    public Rigidbody cubeRigidbody;
    public GameObject enemyGameObject;
    public float smoothTime = 0.1f;

    private Vector3 lastMovingVelocity;
    private Vector3 targetPosition;



    private void Start()
    {
        StartCoroutine(CubeJump());
    }

    public void Chase(Cube enemy)
    {
        transform.position = Vector3.MoveTowards(transform.position, enemy.transform.position, cube.cubeSpeed * Time.deltaTime);
    }

    public void Run(Cube enemy)
    {
        Vector3 dir = enemy.transform.position - transform.position;
        transform.position = Vector3.MoveTowards(transform.position, -dir, cube.cubeSpeed * Time.deltaTime);
    }

    private Vector3 _currentIdleVelocity;
    private float _currentIdleTime;
    private float _maxIdleTime;

    public void Idle()
    {
        _currentIdleTime += Time.deltaTime;

        if (_currentIdleTime > _maxIdleTime)
        {
            // _maxIdleTime 초마다 실행되는 코드.
            _currentIdleVelocity = Random.onUnitSphere;
            _currentIdleVelocity.y = 0f;
            _currentIdleVelocity = _currentIdleVelocity.normalized * (cube.cubeSpeed * 1.3f);
            _currentIdleTime = 0f;
            _maxIdleTime = Random.Range(0.5f, 1.5f);

        }

        cubeRigidbody.velocity = _currentIdleVelocity;
    }

    public Cube FindClosestEnemy()
    {
        Cube enemy;
        float shortDis;
        Collider[] colliders = Physics.OverlapSphere(transform.position, 8f * transform.localScale.x);
        
        enemy = null;
        shortDis = Mathf.Infinity;

        foreach(Collider col in colliders)
        {
            Cube cubeOfCol = col.GetComponentInParent<Cube>();
            if (cubeOfCol == cube) continue;
            if (cubeOfCol == null) continue;
            float distance = Vector3.Distance(transform.position, col.transform.position);

            if (distance < shortDis)
            {
                enemy = cubeOfCol;
                shortDis = distance;
            }
        }

        return enemy;
    }

    IEnumerator CubeJump()
    {
        while (true)
        {
            float ranTime = Random.Range(3f, 5f);
            cube.GetComponent<Rigidbody>().AddForce(new Vector3(0, 600f, 0));
            yield return new WaitForSeconds(ranTime);
        }
        
    }

    private void Update()
    {
        Cube enemy = FindClosestEnemy();
        if (enemy == null)
        {
            Idle();
            return;
        }

        if (enemy.CanEat(cube))
        {
            Run(enemy);
        }
        else if (cube.CanEat(enemy))
        {
            Chase(enemy);
        }
        else
        {
            Idle();
        }
    }
}
