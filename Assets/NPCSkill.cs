using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSkill : MonoBehaviour
{
    public Cube enemy;
    public bool CoolOn;
    public float BanishCoolTime;
    public float banishPower;
    private void Start()
    {
        enemy = null;
        CoolOn = true;
        banishPower = 6000f;
        StartCoroutine(BanishTime());

    }

    IEnumerator BanishTime()
    {
        while (true)
        {
            if (CoolOn && enemy != null)
            {
                Banish(enemy);
                CoolOn = false;
                enemy = null;
                yield return new WaitForSeconds(BanishCoolTime);
                CoolOn = true;

            }
            yield return new WaitForSeconds(0.3f);
        }
    }

    void Banish(Cube enemy)
    {
        Vector3 dir = enemy.transform.position - transform.position;
        enemy.GetComponent<Rigidbody>().AddForce(dir.x * banishPower,dir.y * 1, dir.z * banishPower);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (CoolOn)
        {
            enemy = other.GetComponent<Cube>();
        }
        
    }
}
