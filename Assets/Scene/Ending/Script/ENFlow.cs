using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ENFlow : Flow
{
    public GameObject moveMessage;
    public ENPlayer player;
    
    public Transform veryEndPlayerTransform;
    public GameObject veryEndMap;

    public ENPlayerCamera cam;
    
    public CanvasGroup blackFade;
    public CanvasGroup whiteFade;

    public ENCollisionReporter lightDoorCollider;

    public AudioClip defaultAmbience;
    public AudioClip caveAmbience;
    public AudioClip veryEndSong;

    public GameObject credits;

    public GameObject destroyedBossCubeScene;
    
    private void Start()
    {
        StartFlow();
    }

    protected override IEnumerator FlowRoutine()
    {
        yield return BlackFadeIn();
        player.canMove = false;
        BGMManager.instance.desiredClip = defaultAmbience;
        yield return new WaitForSeconds(1.5f);
        moveMessage.SetActive(true);
        player.canMove = true;
        yield return new WaitForSeconds(3f);
        moveMessage.SetActive(false);
        yield return new WaitForSeconds(2f);
        yield return PrintDialogRoutine("바깥피막");
        yield return AskChoiceRoutine("의미");
        BGMManager.instance.desiredClip = caveAmbience;
        yield return BlackFadeOut();
        player.canMove = false;
        destroyedBossCubeScene.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        yield return BlackFadeIn();
        yield return new WaitForSeconds(1f);
        yield return PrintDialogRoutine("모르겠어");
        yield return new WaitForSeconds(1f);
        BGMManager.instance.desiredClip = defaultAmbience;
        yield return BlackFadeOut();
        destroyedBossCubeScene.SetActive(false);
        player.transform.position = veryEndPlayerTransform.position;
        BGMManager.instance.desiredClip = veryEndSong;
        veryEndMap.SetActive(true);
        cam.UpdateImmediately();
        yield return new WaitForSeconds(2f);
        yield return BlackFadeIn();
        yield return AskChoiceRoutine("안녕");
        if (lastChoice == 1)
        {
            yield return PrintDialogRoutine("일이좋아");
        }
        yield return new WaitForSeconds(2f);
        yield return PrintDialogRoutine("돌아와");
        yield return new WaitForSeconds(0.5f);
        moveMessage.SetActive(true);
        player.canMove = true;
        yield return new WaitUntil(() => lightDoorCollider.didEnterTrigger);
        yield return WhiteFadeOut();
        yield return new WaitForSeconds(2f);
        credits.SetActive(true);
    }

    protected override IEnumerator ShowDialogBoxRoutine(bool isShow)
    {
        DialogManager.instance.dialogBox.SetActive(isShow);
        yield return new WaitForSeconds(0.5f);
    }

    private IEnumerator BlackFadeIn()
    {
        for (float t = 0; t < 1; t += Time.unscaledDeltaTime)
        {
            blackFade.alpha = 1 - t;
            yield return null;
        }
        blackFade.alpha = 0;
    }

    private IEnumerator BlackFadeOut()
    {
        for (float t = 0; t < 1; t += Time.unscaledDeltaTime)
        {
            blackFade.alpha = t;
            yield return null;
        }
        blackFade.alpha = 1;
    }
    
    private IEnumerator WhiteFadeIn()
    {
        for (float t = 0; t < 1; t += Time.unscaledDeltaTime * 0.3f)
        {
            whiteFade.alpha = 1 - t;
            yield return null;
        }
        whiteFade.alpha = 0;
    }

    private IEnumerator WhiteFadeOut()
    {
        for (float t = 0; t < 1; t += Time.unscaledDeltaTime * 0.3f)
        {
            whiteFade.alpha = t;
            yield return null;
        }
        whiteFade.alpha = 1;
    }
}
