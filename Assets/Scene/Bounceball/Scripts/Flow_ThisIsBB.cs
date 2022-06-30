using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;

public class Flow_ThisIsBB : Flow
{
    public GameObject guide;
    public GameObject player;
    public GameObject tutorial;
    public GameObject cinemachineManager;

    private BBPlayerManager _playerMove;

    protected override IEnumerator FlowRoutine()
    {
        DialogManager.instance.enableDialogBoxAnimation = false;
        _playerMove = player.GetComponent<BBPlayerManager>();

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

        yield return new WaitUntil(() => BBGameManager.instance.currentSceneNum == 1);

        tutorial.transform.GetChild(0).GetComponent<Text>().text = "    특수능력: F";
        yield return new WaitForSeconds(1);
        yield return PrintDialogRoutine("특수능력");

        yield return new WaitUntil(() => BBGameManager.instance.currentSceneNum == 2);
        tutorial.gameObject.SetActive(false);
        yield return new WaitForSecondsRealtime(1);
        yield return PrintDialogRoutine("특수타일0");

        cinemachineManager.transform.GetChild(0).gameObject.SetActive(true);
        yield return new WaitUntil(() => !(cinemachineManager.transform.GetChild(0).gameObject.activeSelf));
        yield return PrintDialogRoutine("특수타일1");




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
