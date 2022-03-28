using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerSkill : MonoBehaviour
{
    
    public bool _canUseTimeStop = true;
    public bool timeStopStart = false;
    public bool toggle = false;
    public float TimeStopCoolTime = 10f;
    public float stoppingTime = 2f;
    public List<Cube> enemys;
    public AudioSource music;


    private PlayerMove playerMove;
    private Cube myCube;

    private void Awake()
    {
        playerMove = GetComponentInParent<PlayerMove>();
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
        timeStopStart = false;
        toggle = true;
    }

    private IEnumerator CoroutineCooldownTimer()
    {
        _canUseTimeStop = false;
        playerMove.particlesLight.color = Color.yellow;
        yield return new WaitForSecondsRealtime(TimeStopCoolTime);
        _canUseTimeStop = true;
        playerMove.particlesLight.color = Color.green;
    }

    public void timeStop()
    {
        timeStopStart = true;
        StartCoroutine(CoroutineCooldownTimer());
        for(int i = 0; i < enemys.Count; i++)
        {
            enemys[i].GetComponent<Rigidbody>().isKinematic = true;
            enemys[i].GetComponent<CubeBehavior>().enabled = false;
            enemys[i].enabled = false;
        }
        
        StartCoroutine(StoppingTime());
    }

    private void Update()
    {
        if (timeStopStart)
        {
            pitchDown();
            
        }
        else if (toggle)
        {
            pitchUp();
        }
    }

    private void pitchDown()
    {
        if(music.pitch > 0)
        {
            music.pitch -= Time.deltaTime;
        }
    }

    private void pitchUp()
    {
        if(music.pitch < 1)
        {
            music.pitch += 1.3f * Time.deltaTime;
        }
        else if(music.pitch > 1)
        {
            music.pitch = 1;
            toggle = false;
        }
    }

    
    
}
