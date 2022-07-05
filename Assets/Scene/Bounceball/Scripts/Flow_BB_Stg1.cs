using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flow_BB_Stg1: Flow
{
    public GameObject player;
    public GameObject cams;

    private BBPlayer _player;
    private int _deadNum => PlayerPrefs.GetInt("deadNum");


    protected override IEnumerator FlowRoutine()
    {
        yield break;
        DialogManager.instance.enableDialogBoxAnimation = false;
        _player = player.GetComponent<BBPlayer>();

        if(_deadNum == 0)
        {
            yield return PrintDialogRoutine("스테이지시작");

            player.GetComponent<BBPlayerMovement>().playerMoveOn = false;

            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));

            yield return PrintDialogRoutine("스페이스바설명");

            cams.GetComponent<BBCamControl>().wideCam.SetActive(false);
        }
        else if(_deadNum == 1)
        {
            yield return PrintDialogRoutine("죽음1");
        }
        else if(_deadNum == 2)
        {
            yield return PrintDialogRoutine("죽음2");
        }
        else if(_deadNum == 3)
        {
            yield return PrintDialogRoutine("죽음3");
        }
        else if(_deadNum == 4)
        {
            yield return PrintDialogRoutine("죽음4");

            yield return AskChoiceRoutine("죽음4");

            if (lastChoice == 0)
            {
                yield return PrintDialogRoutine("죽음4_1");
                player.GetComponent<BBPlayerAbility>().guideAbilityOn = true;
            }
            else if (lastChoice == 1)
            {
                yield return PrintDialogRoutine("죽음4_2");
                player.GetComponent<BBPlayerAbility>().guideAbilityOn = true;
            }

        }
        else if (_deadNum >= 5)
        {
            player.GetComponent<BBPlayerAbility>().guideAbilityOn = true;
        }
        


        yield break;
    }

    protected override IEnumerator PrintDialogRoutine(string dialogName)
    {
        _player.movement.playerMoveOn = false;
        yield return base.PrintDialogRoutine(dialogName);
        _player.movement.playerMoveOn = true;
    }
    protected override IEnumerator AskChoiceRoutine(string selectionName)
    {
        Time.timeScale = 0;
        _player.movement.playerMoveOn = false;
        yield return base.AskChoiceRoutine(selectionName);
        _player.movement.playerMoveOn = true;
        Time.timeScale = 1;
    }
}
