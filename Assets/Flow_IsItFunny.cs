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

    protected override IEnumerator FlowRoutine()
    {
        yield return new WaitForSeconds(1f); // 8f

        playerCloseupCam.SetActive(true);

        yield return StopTimeAnimatedRoutine();
        
        yield return new WaitForSecondsRealtime(1.5f);

        yield return PrintDialogRoutine("재밌나요");
        yield return AskChoiceRoutine("재밌나요");


        yield return PrintDialogRoutine("뭐가불만인가요");
        yield return AskChoiceRoutine("뭐가불만인가요");
        
        yield return PrintDialogRoutine("그렇게보이진않는데");
        yield return AskChoiceRoutine("그렇게보이진않는데");

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

        yield return new WaitUntil(() => 
            Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return));
        
        resistMessage.SetActive(true);
        PlayerControl pc = player.GetComponent<PlayerControl>();
        pc.isCanResist = true;
        yield return new WaitUntil(() => pc.resistNum >= 7);

        yield return StopTimeAnimatedRoutine();
        yield return new WaitForSecondsRealtime(1.5f);

        resistMessage.SetActive(false);
        yield return PrintDialogRoutine("방해하지마");
        yield return AskChoiceRoutine("방해하지마");
        
        yield return PrintDialogRoutine("첫번째분기점");
        yield return AskChoiceRoutine("첫번째분기점");

        int selected = GetLastSelection();
        if (selected == 0)
        {
            yield return PrintDialogRoutine("탈출");
        }
        else if (selected == 1)
        {
            yield return PrintDialogRoutine("현실에안주");
        }
    }
}
