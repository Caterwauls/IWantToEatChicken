using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill : MonoBehaviour
{
    
    public bool _canUseTimeStop = true;
    public float TimeStopCoolTime = 10f;
    public float stoppingTime = 2f;
    public List<Cube> enemys;


    private Cube myCube;

    private void Awake()
    {
        myCube = GetComponent<Cube>();
        enemys = GameManager.instance.enemyCubes;
    }

    private IEnumerator StoppingTime()
    {
        yield return new WaitForSecondsRealtime (stoppingTime);
        for (int i = 0; i < enemys.Count; i++)
        {
            enemys[i].GetComponent<Rigidbody>().isKinematic = false;
            enemys[i].GetComponent<CubeBehavior>().enabled = true;
            enemys[i].enabled = true;
        }

    }

    private IEnumerator CoroutineCooldownTimer()
    {
        _canUseTimeStop = false;
        yield return new WaitForSecondsRealtime(TimeStopCoolTime);
        _canUseTimeStop = true;
        
    }

    public void timeStop()
    {
        
        StartCoroutine(CoroutineCooldownTimer());
        for(int i = 0; i < enemys.Count; i++)
        {
            enemys[i].GetComponent<Rigidbody>().isKinematic = true;
            enemys[i].GetComponent<CubeBehavior>().enabled = false;
            enemys[i].enabled = false;
        }
        
        StartCoroutine(StoppingTime());
    }
    
    
}
