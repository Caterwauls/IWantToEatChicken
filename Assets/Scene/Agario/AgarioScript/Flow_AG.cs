using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flow_AG : Flow
{
    public bool isDialogEnd = false;
    public GameObject initWhenDialogEnded;
    public GameObject inGameUi;
    public GameObject guideArrowUi;
    public bool didSeeGuide = false;
    public bool startGame = false;
    public GameObject pressEsc;

    private int _deadNum => PlayerPrefs.GetInt("deadNum");



    protected override IEnumerator FlowRoutine()
    {

        if (_deadNum == 0)
        {
            yield return new WaitForSecondsRealtime(3f);
            Time.timeScale = 0;

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
            Time.timeScale = 0;
            yield return new WaitForSecondsRealtime(1f);
            yield return PrintDialogRoutine("1데스");

        }
        else if (_deadNum == 2)
        {
            Time.timeScale = 0;
            yield return new WaitForSecondsRealtime(1f);
            yield return PrintDialogRoutine("2데스");

        }
        else
        {
            Time.timeScale = 0;
            yield return new WaitForSecondsRealtime(1f);
            yield return PrintDialogRoutine("3데스");


        }
        inGameUi.SetActive(true);
        initWhenDialogEnded.SetActive(true);
        isDialogEnd = true;
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


