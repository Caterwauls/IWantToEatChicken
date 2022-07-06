using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flow_BB_Stg1: Flow
{
    public GameObject player;
    public GameObject cams;

    private BBPlayer _player;
    
    protected override IEnumerator FlowRoutine()
    {
        yield break;
        DialogManager.instance.enableDialogBoxAnimation = false;
        _player = player.GetComponent<BBPlayer>();

            yield return PrintDialogRoutine("스테이지시작");

            player.GetComponent<BBPlayerMovement>().playerMoveOn = false;

            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));

            yield return PrintDialogRoutine("스페이스바설명");

            cams.GetComponent<BBCamControl>().wideCam.SetActive(false);

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
