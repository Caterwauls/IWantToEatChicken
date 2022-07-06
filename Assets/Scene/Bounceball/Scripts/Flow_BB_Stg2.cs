using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flow_BB_Stg2 : Flow
{
    private BBPlayer _player;

    protected override IEnumerator FlowRoutine()
    {
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
