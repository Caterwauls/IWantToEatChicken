using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
public class PlayerSkill : MonoBehaviour
{

    public bool _canUseTimeStop = true;
    public bool timeStopStart = false;
    public bool toggle = false;
    public float TimeStopCoolTime = 10f;
    public float stoppingTime = 2f;
    public List<Cube> enemys;
    public AudioSource music;
    public AudioSource tiktak;
    public PostProcessVolume stopEffect;
    public float x = 0;



    private PlayerMove playerMove;
    private Cube myCube;
    private ParticleSystem shockwave;

    private void Awake()
    {
        playerMove = GetComponentInParent<PlayerMove>();
        myCube = GetComponent<Cube>();
        shockwave = GetComponentInChildren<ParticleSystem>();
    }

    private void Start()
    {
        enemys = GameManager.instance.enemyCubes;
    }

    private IEnumerator StoppingTime()
    {
        yield return new WaitForSecondsRealtime(stoppingTime);
        for (int i = 0; i < enemys.Count; i++)
        {
            enemys[i].GetComponent<Rigidbody>().isKinematic = false;
            enemys[i].GetComponent<CubeBehavior>().enabled = true;
            enemys[i].enabled = true;
        }
        shockwave.Stop();
        tiktak.Stop();
        stopEffect.profile.GetSetting<Bloom>().active = false;
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
        tiktak.pitch = 0.85f;
        timeStopStart = true;
        stopEffect.profile.GetSetting<Bloom>().active = true;
        shockwave.Play();
        tiktak.Play();
        StartCoroutine(CoroutineCooldownTimer());
        for (int i = 0; i < enemys.Count; i++)
        {
            if(enemys[i] != null)
            {
                enemys[i].GetComponent<Rigidbody>().isKinematic = true;
                enemys[i].GetComponent<CubeBehavior>().enabled = false;
                enemys[i].enabled = false;
            }
            
        }

        StartCoroutine(StoppingTime());
    }

    private void Update()
    {
        if (timeStopStart)
        {
            pitchDown();
            StopEffect();

        }
        else if (toggle)
        {
            pitchUp();
            StartEffectAfterStop();
        }
        tiktakPicth();

    }

    private void pitchDown()
    {
        if (music.pitch > 0)
        {
            music.pitch -= 0.8f * Time.deltaTime;
        }
    }

    private void pitchUp()
    {
        if (music.pitch < 1)
        {
            music.pitch += 1.3f * Time.deltaTime;
        }
        else if (music.pitch > 1)
        {
            music.pitch = 1;
            toggle = false;
        }
    }

    private void tiktakPicth()
    {
        if(tiktak.pitch > 0)
        {
            tiktak.pitch -= 0.33f * Time.deltaTime;
        }
    }

    private void StopEffect()
    {

        
        x += 1.5f * Time.deltaTime;
        if (x >= 0.7)
        {
            x = 0.75f;
            return;
        }
        stopEffect.profile.GetSetting<Vignette>().intensity.Override(x);
    }

    private void StartEffectAfterStop()
    {
        x -= 1.5f * Time.deltaTime;
        if (x <= 0)
        {
            x = 0;
            return;
        }
        stopEffect.profile.GetSetting<Vignette>().intensity.Override(x);
    }


}
