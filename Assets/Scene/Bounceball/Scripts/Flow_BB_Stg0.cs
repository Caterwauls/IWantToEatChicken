using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;

public class Flow_BB_Stg0 : Flow
{
    public GameObject guide;
    public GameObject player;
    public GameObject tutorial;
    public GameObject cinemachineManager;
    public GameObject abilityUI;

    private BBPlayer _playerMove;

    protected override IEnumerator FlowRoutine()
    {
        DialogManager.instance.enableDialogBoxAnimation = false;
        _playerMove = player.GetComponent<BBPlayer>();
        _playerMove.movement.playerMoveOn = false;
        yield return new WaitForSeconds(2f);
        guide.SetActive(true);
        yield return new WaitForSeconds(3f);
        yield return PrintDialogRoutine("통통");
        tutorial.SetActive(true);
        yield return new WaitUntil(() => BBGameManager.instance.currentSceneNum == 1);

        tutorial.transform.GetChild(0).GetComponent<Text>().text = "    Special Ability: F";
        yield return new WaitUntil(() => BBGameManager.instance.currentSceneNum == 2);
        tutorial.gameObject.SetActive(false);
    }

    protected override IEnumerator PrintDialogRoutine(string dialogName)
    {
        DialogManager.instance.dialogBox.SetActive(true);
        _playerMove.movement.playerMoveOn = false;
        yield return base.PrintDialogRoutine(dialogName);
        _playerMove.movement.playerMoveOn = true;
        DialogManager.instance.dialogBox.SetActive(false);
    }
    protected override IEnumerator AskChoiceRoutine(string selectionName)
    {
        Time.timeScale = 0;
        _playerMove.movement.playerMoveOn = false;
        yield return base.AskChoiceRoutine(selectionName);
        _playerMove.movement.playerMoveOn = true;
        Time.timeScale = 1;
    }
}
