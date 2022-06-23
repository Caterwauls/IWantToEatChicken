using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flow_IsItFunny : Flow
{
    public GameObject playerCloseupCam;
    public GameObject otherCamera;
    public GameObject otherPingPong;
    public GameObject resistMessage;
    public GameObject player;
    public GameObject EndMessage;
    public GameObject restartMessage;
    public SpacePressEffectOn spaceEffect;
    public GameObject cutSceneDirector;

    public bool debugOn;


    protected override IEnumerator FlowRoutine()
    {
        PlayerControl pc = player.GetComponent<PlayerControl>();

        yield return new WaitForSeconds(1f); // 8f

        playerCloseupCam.SetActive(true);

        yield return StopTimeAnimatedRoutine();




        if (!debugOn)
        {

            yield return new WaitForSecondsRealtime(1.5f);

            yield return PrintDialogRoutine("재밌나요");
            yield return AskChoiceRoutine("재밌나요");

            if (lastChoice == 0)
            {
                yield return PrintDialogRoutine("아닌거같은데");
            }


            yield return PrintDialogRoutine("뭐가불만인가요");
            yield return AskChoiceRoutine("뭐가불만인가요");

            yield return PrintDialogRoutine("그렇게보이진않는데");

            otherCamera.SetActive(true);

            Time.timeScale = 1;
            otherPingPong.SetActive(true);

            yield return new WaitForSecondsRealtime(4f);

            yield return PrintDialogRoutine("다른사람들도똑같아");

            otherCamera.SetActive(false);

            yield return new WaitForSecondsRealtime(2f);

            yield return StopTimeAnimatedRoutine();

            yield return new WaitForSecondsRealtime(1.5f);

            yield return PrintDialogRoutine("다들하는대로해라");
            Time.timeScale = 1;

            playerCloseupCam.SetActive(false);
            yield return new WaitForSecondsRealtime(2f);

            yield return new WaitUntil(() =>
                Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return));

            resistMessage.SetActive(true);
            
            pc.isCanResist = true;
            spaceEffect.canPlayEffect = true;
            yield return new WaitUntil(() => pc.resistNum >= 10);

            resistMessage.SetActive(false);
            spaceEffect.canPlayEffect = false;
            playerCloseupCam.SetActive(true);

            yield return StopTimeAnimatedRoutine();
            yield return new WaitForSecondsRealtime(1.5f);

            pc.isCanResist = false;

            yield return PrintDialogRoutine("방해하지마");
            yield return AskChoiceRoutine("방해하지마");

            yield return new WaitForSecondsRealtime(2f);

            yield return PrintDialogRoutine("탈출하고싶나요");
            yield return AskChoiceRoutine("탈출하고싶나요");

            if (lastChoice == 0)
            {
                yield return PrintDialogRoutine("탈출하고싶다");
            }
            else if (lastChoice == 1)
            {
                yield return PrintDialogRoutine("탈출하고싶지않다");
                yield return AskChoiceRoutine("탈출하고싶지않다");

                if (lastChoice == 0)
                {
                    yield return PrintDialogRoutine("탈출하고싶지않다1");
                    playerCloseupCam.SetActive(false);
                    pc.isCanResist = false;
                    Time.timeScale = 1;
                    DialogManager.instance.blurEffect.SetActive(true);
                    EndMessage.SetActive(true);
                    yield return new WaitUntil(() => (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space)));

                    restartMessage.SetActive(true);
                    yield break;

                }
                else if (lastChoice == 1)
                {
                    yield return PrintDialogRoutine("탈출하고싶지않다2");
                    yield return new WaitForSecondsRealtime(1f);
                }
            }
        }

        yield return new WaitForSecondsRealtime(1f);

        yield return PrintDialogRoutine("첫분기점");
        yield return AskChoiceRoutine("첫분기점");

        if (lastChoice == 0)
        {
            yield return PrintDialogRoutine("탈출");
            Time.timeScale = 1f;
            playerCloseupCam.SetActive(false);
            otherPingPong.SetActive(false);
            player.GetComponent<Rigidbody>().velocity = Vector3.zero;
            player.GetComponent<Rigidbody>().MovePosition(new Vector3(0.06f, 0.84f, 0));
            cutSceneDirector.SetActive(true);
        }
        else if (lastChoice == 1)
        {
            yield return PrintDialogRoutine("현실에안주");
            playerCloseupCam.SetActive(false);
            pc.isCanResist = false;
            Time.timeScale = 1;
            DialogManager.instance.blurEffect.SetActive(true);
            EndMessage.SetActive(true);
            yield return new WaitUntil(() => (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space)));

            restartMessage.SetActive(true);
            yield break;
        }
    }
}
