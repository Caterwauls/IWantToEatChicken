using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class Cube : MonoBehaviour
{

    public float energy;
    public float cubeSpeed = 7f;
    public bool isTriggerOn = false;

    private void Start()
    {
        UpdateScale();
    }

    private void UpdateScale()
    {
        transform.localScale = Vector3.one * Mathf.Pow(energy, 1 / 3f);
    }

    public void AbsorbPowerFrom(Cube otherCube)
    {
        float absorbSpeed = energy / 10 + 10;

        otherCube.energy -= absorbSpeed * Time.fixedDeltaTime;
        energy += absorbSpeed * Time.fixedDeltaTime;
    }




    protected void OnTriggerStay(Collider other)
    {
        // other가 CubeCollider가 아닐 경우 무시
        if (other.tag != "CubeCollider") return;

        // 만약 자기 자신의 cubeCollider일 경우 무시
        if (other.transform.parent == transform) return;
        if (other.GetComponentInParent<Cube>().energy < 0.7f * energy)
        {
            isTriggerOn = true;
            Cube otherCube = other.GetComponentInParent<Cube>();
            AbsorbPowerFrom(otherCube);


            UpdateScale();

            if (otherCube.energy <= 0)
            {
                Destroy(otherCube.gameObject);
            }
            else
            {           
                otherCube.UpdateScale();
            }

        }
    }
    void EnemyCheck()
    {
        Collider[] enemyInLength = Physics.OverlapSphere(transform.position, energy * 8f, 8);

        GameObject enemyGameObject = enemyInLength[0].gameObject;
        float shortDis = Vector3.Distance(enemyInLength[0].transform.position, transform.position);

        for (int i = 0; i < enemyInLength.Length; i++)
        {
            if (enemyInLength[i].tag == "Cube")
            {
                float temp = Vector3.Distance(enemyInLength[i].transform.position, transform.position);
                if (temp < shortDis)
                {
                    enemyGameObject = enemyInLength[i].gameObject;
                    shortDis = temp;
                }

            }
        }

    }



}
