using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;

public class BBPlayerAbility : BBPlayerComponentBase
{
    public float dashAbilityDis = 10f;
    public bool dashAbilityOn = false;
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
            UseDashAbility();

        }
        else if (flyAbilityOn)
        {
            UseFlyAbility();
        }
        else if (doubleJumpOn)
        {
            useDoubleJump();
        }

        if(Input.GetKeyDown(KeyCode.G) && guideAbilityOn)
        {
            guideAbilityOn = false;
            var currentTime = Time.unscaledTime;
            StartCoroutine(guideAbility(currentTime, BBGameManager.instance.currentStg));
            

        }
        else if(Input.GetKeyDown(KeyCode.G) && !guideAbilityOn)
        {
            StartCoroutine(showGuideAbilityText());
        }
    }

    IEnumerator guideAbility(float currentTime,int currentStg)
    {
        if (currentStg == 0) yield break;
        else if(currentStg == 1)
        {
            var a = Instantiate(guideAbilityEffect);
            a.transform.position = transform.position;
            flyAbilityOn = true;
            UseFlyAbility();

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
    
    IEnumerator showGuideAbilityText()
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

    void UseDashAbility()
    {
        var inputVec = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if (Input.GetKeyDown(KeyCode.F))
        {
            if (inputVec.x < 0)
            {
                // 왼쪽
                Instantiate(dashAbilityEffect, transform.position,
                    Quaternion.Euler(dashAbilityEffect.transform.rotation.x, dashAbilityEffect.transform.rotation.x - 90, dashAbilityEffect.transform.rotation.x));
                transform.position = transform.position + Vector3.left * dashAbilityDis;
                dashAbilityOn = false;
            }
            else if (inputVec.x >= 0)
            {
                // 오른쪽
                Instantiate(dashAbilityEffect, transform.position,
                    Quaternion.Euler(dashAbilityEffect.transform.rotation.x, dashAbilityEffect.transform.rotation.x + 90, dashAbilityEffect.transform.rotation.x));
                transform.position = transform.position + Vector3.right * dashAbilityDis;
                dashAbilityOn = false;

            }
        }

    }

    void UseFlyAbility()
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

    void useDoubleJump()
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
