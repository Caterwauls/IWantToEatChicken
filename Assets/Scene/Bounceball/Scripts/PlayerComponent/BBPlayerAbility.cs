using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;

public class BBPlayerAbility : BBPlayerComponentBase
{
    private static Collider[] _colBuffer = new Collider[128];
    
    public float dashAbilityDis = 10f;
    public bool dashAbilityOn = false;
    public float dashDuration = 1f;
    public int dashSweepCount = 15;
    public GameObject dashAbilityEffect;

    public bool flyAbilityOn = false;
    public GameObject flyAbility;
    public GameObject flyAbilityStartEffect;

    public bool guideAbilityOn = false;
    public bool canUseGuideAbility = true;
    public GameObject guideAbilityEffect;
    public GameObject guideAbilityText;
    public float guideAbilityCoolTime = 5;
    public string remainGuideAbilityTime;

    public bool doubleJumpOn = false;
    public GameObject doubleJumpEffect;

    public GameObject timeStopVolume;
    public GameObject timeStopWave;

    private void Update()
    {
        if (dashAbilityOn)
        {
            CheckDashAbility();
        }
        else if (flyAbilityOn)
        {
            CheckFlyAbility();
        }
        else if (doubleJumpOn)
        {
            CheckDoubleJump();
        }

        if(Input.GetKeyDown(KeyCode.G) && guideAbilityOn)
        {
            guideAbilityOn = false;
            var currentTime = Time.unscaledTime;
            StartCoroutine(GuideAbilityRoutine(currentTime, BBGameManager.instance.currentStg));
            

        }
        else if(Input.GetKeyDown(KeyCode.G) && !guideAbilityOn)
        {
            StartCoroutine(ShowGuideAbilityTextRoutine());
        }
    }

    IEnumerator GuideAbilityRoutine(float currentTime,int currentStg)
    {
        if (currentStg == 0) yield break;
        else if(currentStg == 1)
        {
            var a = Instantiate(guideAbilityEffect);
            a.transform.position = transform.position;
            flyAbilityOn = true;
            CheckFlyAbility();

            while (true)
            {
                if (Time.unscaledTime - currentTime > guideAbilityCoolTime)
                {
                    guideAbilityOn = true;
                    yield break;
                }
                var remainTime = guideAbilityCoolTime - (Time.unscaledTime - currentTime);
                remainGuideAbilityTime = remainTime.ToString("0.0");

                yield return null;
            }

        }
        else if(currentStg == 2)
        {
            Time.timeScale = 0.5f;
            timeStopVolume.SetActive(true);
            var a = Instantiate(timeStopWave);
            a.transform.position = transform.position;

            while (true)
            {
                if(Time.unscaledTime - currentTime > 2)
                {
                    Time.timeScale = 1;
                    timeStopVolume.SetActive(false);
                }
                if (Time.unscaledTime - currentTime > guideAbilityCoolTime)
                {
                    guideAbilityOn = true;
                    yield break;
                }
                var remainTime = guideAbilityCoolTime - (Time.unscaledTime - currentTime);
                remainGuideAbilityTime = remainTime.ToString("0.0");

                yield return null;
            }

        }
        

    }
    
    IEnumerator ShowGuideAbilityTextRoutine()
    {
        guideAbilityText.GetComponent<Text>().text = remainGuideAbilityTime + "초 남았습니다.";
        guideAbilityText.SetActive(true);
        while(guideAbilityText.GetComponent<Text>().color.a < 1)
        {
            var currentColor = guideAbilityText.GetComponent<Text>().color;
            currentColor.a += 2f * 0.01f;
            guideAbilityText.GetComponent<Text>().color = currentColor;
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(0.5f);

        while (guideAbilityText.GetComponent<Text>().color.a > 0)
        {
            var currentColor = guideAbilityText.GetComponent<Text>().color;
            currentColor.a -= 2f * 0.01f;
            guideAbilityText.GetComponent<Text>().color = currentColor;
            yield return new WaitForSeconds(0.01f);
        }
        guideAbilityText.SetActive(false);
        yield break;
    }

    void CheckDashAbility()
    {
        if (!Input.GetKeyDown(KeyCode.F)) return;
        
        Instantiate(dashAbilityEffect, transform.position, Quaternion.identity);
        dashAbilityOn = false;

        Vector3 sweepDir = Input.GetAxis("Horizontal") < 0 ? Vector3.left : Vector3.right;
        Vector3 start = transform.position;
        Vector3 destination = transform.position;

        for (int i = 0; i < dashSweepCount; i++)
        {
            var candidate = transform.position + dashAbilityDis * i / (dashSweepCount - 1f) * sweepDir;
            if (Physics.OverlapBoxNonAlloc(candidate, Vector3.one * 0.4f, _colBuffer, Quaternion.identity) > 0) 
                continue;
            destination = candidate;
        }
        
        StartCoroutine(InterpolatePosition());
        IEnumerator InterpolatePosition()
        {
            _rb.isKinematic = true;
            for (float t = 0; t < dashDuration; t += Time.deltaTime)
            {
                transform.position = Vector3.Lerp(start, destination, t / dashDuration);
                yield return null;
            }
            _rb.velocity = Vector3.zero;
            _rb.isKinematic = false;
        }
    }

    void CheckFlyAbility()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            flyAbilityOn = false;
            GetComponent<MeshRenderer>().enabled = false;
            _player.movement.playerMoveOn = false;
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            GetComponent<Rigidbody>().isKinematic = true;
            Instantiate(flyAbilityStartEffect, transform.position, Quaternion.identity);
            Instantiate(flyAbility, transform.position, Quaternion.identity);
        }

    }

    void CheckDoubleJump()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            doubleJumpOn = false;
            var bottomPos = transform.position + Vector3.up * transform.lossyScale.y / 2f;
            Instantiate(doubleJumpEffect, bottomPos, Quaternion.identity);
            Vector3 newVelocity = _rb.velocity;
            newVelocity.y = GetComponent<BBPlayerMovement>().jumpSpeed * 1.5f;
            _rb.velocity = newVelocity;

            if (GetComponent<BBPlayerReverseTile>().reverseOn)
            {
                newVelocity.y = -GetComponent<BBPlayerMovement>().jumpSpeed * 1.5f;
                _rb.velocity = newVelocity;
            }
        }
    }
}
