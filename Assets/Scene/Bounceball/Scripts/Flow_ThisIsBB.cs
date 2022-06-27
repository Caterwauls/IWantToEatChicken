using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flow_ThisIsBB : Flow
{
    public GameObject guide;
    public GameObject player;
    public GameObject tutorial;

    private BBPlayerMove _playerMove;

    protected override IEnumerator FlowRoutine()
    {
        DialogManager.instance.enableDialogBoxAnimation = false;
        _playerMove = player.GetComponent<BBPlayerMove>();

        _playerMove.playerMoveOn = false;

        yield return new WaitForSeconds(2f);

        guide.SetActive(true);

        yield return new WaitForSeconds(3f);

        yield return PrintDialogRoutine("여기는공튀기기");
        yield return AskChoiceRoutine("여기는공튀기기");

        if (lastChoice == 0)
        {
            yield return PrintDialogRoutine("튜토리얼");
            tutorial.SetActive(true);
        }
        else if (lastChoice == 1)
        {
            yield return PrintDialogRoutine("그냥한다");

        }


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
