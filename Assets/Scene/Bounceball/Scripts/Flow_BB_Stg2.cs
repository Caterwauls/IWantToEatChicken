using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flow_BB_Stg2 : Flow
{
    public GameObject player;
    public GameObject cams;

    private BBPlayer _player;
    private int _deadNum => PlayerPrefs.GetInt("deadNum");


    protected override IEnumerator FlowRoutine()
    {
        DialogManager.instance.enableDialogBoxAnimation = false;
        _player = player.GetComponent<BBPlayer>();


        player.GetComponent<BBPlayerAbility>().guideAbilityOn = true;


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
