using System.Collections;
using System.Collections.Generic;
using UnityEngine;



// 여기서쓴 온트리거는 큐브에 달려있는 구형 빈 게임 오브젝트에 충돌감지로 
// 다른 큐브를 감지하고 그 큐브와 자신의 크기를 비교하여 도망가거나 쫒아가거나를 결정.

public class CubeBehavior : MonoBehaviour
{

    public float maxSize = 100f;
    public Cube Cube;


    public void Chase()
    {

    }

    public void Run()
    {

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Cube" && other.GetComponent<Cube>().energy < GetComponent<Cube>().energy)
        {
            Chase();
        }
        else if (other.tag == "Cube" && other.GetComponent<Cube>().energy > GetComponent<Cube>().energy)
        {
            Run();
        }
    }


}
