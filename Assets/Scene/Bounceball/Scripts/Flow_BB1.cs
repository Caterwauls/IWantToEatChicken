using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flow_BB1 : Flow
{
    public GameObject player;

    private BBPlayerManager _playerMove;

    protected override IEnumerator FlowRoutine()
    {
        DialogManager.instance.enableDialogBoxAnimation = false;
        _playerMove = player.GetComponent<BBPlayerManager>();





        yield break;
    }

    protected override IEnumerator PrintDialogRoutine(string dialogName)
    {
        _playerMove.playerMoveOn = false;
        yield return base.PrintDialogRoutine(dialogName);
        _playerMove.playerMoveOn = true;
    }
    protected override IEnumerator AskChoiceRoutine(string selectionName)
    {
        Time.timeScale = 0;
        _playerMove.playerMoveOn = false;
        yield return base.AskChoiceRoutine(selectionName);
        _playerMove.playerMoveOn = true;
        Time.timeScale = 1;
    }
}
