using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Flow_AG : Flow
{
    public bool isDialogEnd = false;
    public GameObject initWhenDialogEnded;
    public GameObject inGameUi;
    public GameObject guideArrowUi;
    public bool didSeeGuide = false;
    public bool startGame = false;
    public GameObject pressEsc;
    public GameObject guideText;

    public static int _deadNum;

    protected override IEnumerator PrintDialogRoutine(string dialogName)
    {
        var dialog = DialogManager.instance.dialogs[dialogName];
        for (int i = 0; i < dialog.lines.Count; i++)
        {
            dialog.lines[i] = dialog.lines[i].Replace("$PLAYERNAME$", PlayerPrefs.GetString("AgarioPlayerName"));
        }
        DialogManager.instance.dialogBox.GetComponent<CanvasGroup>().alpha = 1f;
        yield return base.PrintDialogRoutine(dialogName);
        DialogManager.instance.dialogBox.GetComponent<CanvasGroup>().alpha = 0f;
    }


    protected override IEnumerator FlowRoutine()
    {
        Time.timeScale = 0;

        if (_deadNum == 0)
        {
            yield return new WaitForSecondsRealtime(3f);


            yield return PrintDialogRoutine("무슨");

            yield return AskChoiceRoutine("무슨");

            yield return new WaitForSecondsRealtime(0.5f);

            yield return PrintDialogRoutine("포기");

            Time.timeScale = 1;
            AGGameManager.instance.isCanUseUi = true;
            pressEsc.SetActive(true);
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Escape));
            pressEsc.SetActive(false);
            guideArrowUi.SetActive(true);

            yield return new WaitUntil(() => didSeeGuide);
            guideArrowUi.SetActive(false);
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Escape) || startGame);
            inGameUi.SetActive(true);

        }
        else if (_deadNum == 1)
        {
            yield return new WaitForSecondsRealtime(1f);
            yield return PrintDialogRoutine("1데스");

        }
        else if (_deadNum == 2)
        {
            yield return new WaitForSecondsRealtime(1f);
            yield return PrintDialogRoutine("2데스");

        }
        else
        {
            yield return new WaitForSecondsRealtime(1f);
            yield return PrintDialogRoutine("3데스");

            yield return new WaitForSecondsRealtime(1f);
            pressEsc.GetComponent<Text>().text = "In this stage, you should check the guide";
            guideText.GetComponent<Text>().text = "'F' is player's hidden skill. If game is too hard, use this skill!";
            Time.timeScale = 1;
            AGGameManager.instance.isCanUseUi = true;
            pressEsc.SetActive(true);
            yield return new WaitForSecondsRealtime(5f);
            pressEsc.SetActive(false);
        }
        isDialogEnd = true;
        inGameUi.SetActive(true);
        inGameUi.transform.GetChild(0).gameObject.SetActive(true);
        AGGameManager.instance.isCanUseUi = true;

        initWhenDialogEnded.SetActive(true);

        AGGameManager.instance.isCanPlayerMove = true;
        Time.timeScale = 1;






        yield return new WaitForSecondsRealtime(0.1f);
    }

    public void DidSeeGuide()
    {
        didSeeGuide = true;
    }

    public void FinallyStartGame()
    {
        startGame = true;
    }
}


