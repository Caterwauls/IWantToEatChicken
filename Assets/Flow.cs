using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flow : MonoBehaviour
{
    public void StartFlow()
    {
        StartCoroutine(FlowRoutine());
    }

    protected virtual IEnumerator FlowRoutine()
    {
        yield break;
    }
    
    protected int GetLastSelection()
    {
        return DialogManager.instance.lastChoice;
    }

    private void StartAnswerFlow()
    {
        DialogManager.instance.dialogBox.SetActive(false);
        DialogManager.instance.blurEffect.SetActive(true);
        DialogManager.instance.playerSelectionRing.SetActive(true);
    }

    private void EndAnswerFlow()
    {
        DialogManager.instance.dialogBox.SetActive(true);
        DialogManager.instance.blurEffect.SetActive(false);
        DialogManager.instance.playerSelectionRing.SetActive(false);
    }

    protected IEnumerator StopTimeAnimatedRoutine()
    {
        while (true)
        {
            Time.timeScale -= 0.01f;
            yield return new WaitForSeconds(0.01f);

            if (Time.timeScale <= 0.1f)
            {
                Time.timeScale = 0;
                break;
            }
        }
    }

    private IEnumerator ShowDialogBoxRoutine(bool isShow)
    {
        while (true)
        {
            if (isShow && DialogManager.instance.dialogBoxScaler.scaleFactor < 1)
            {
                DialogManager.instance.dialogBoxScaler.scaleFactor += 0.08f;
                yield return new WaitForSecondsRealtime(0.01f);
            }

            else if (!isShow && DialogManager.instance.dialogBoxScaler.scaleFactor > 0.01f)
            {
                DialogManager.instance.dialogBoxScaler.scaleFactor -= 0.1f;
                yield return new WaitForSecondsRealtime(0.01f);
            }

            else if (DialogManager.instance.dialogBoxScaler.scaleFactor >= 1 || 
                     DialogManager.instance.dialogBoxScaler.scaleFactor <= 0.01f)
            {
                break;
            }
        }
    }

    protected IEnumerator AskChoiceRoutine(string selectionName)
    {
        DialogManager.instance.currentChoice = DialogManager.instance.choices[selectionName];
        DialogManager.instance.didSelect = false;
        StartAnswerFlow();
        yield return new WaitUntil(() => DialogManager.instance.didSelect);
        EndAnswerFlow();
    }

    protected IEnumerator PrintDialogRoutine(string dialogName)
    {
        DialogManager.instance.dialogText.text = "";
        yield return ShowDialogBoxRoutine(true);
        DialogManager.instance.promptEffect.SetActive(false);

        Dialog dialog = DialogManager.instance.dialogs[dialogName];

        for(int i = 0; i < dialog.lines.Count; i++)
        {
            DialogManager.instance.dialogText.text = "";
            string text = dialog.lines[i];
            for (int j = 0; j < text.Length; j++)
            {
                DialogManager.instance.dialogText.text += text[j];
                yield return new WaitForSecondsRealtime(0.1f);
            }
            yield return new WaitForSecondsRealtime(0.3f);

            DialogManager.instance.dialogText.text += "_";
            DialogManager.instance.promptEffect.SetActive(true);

            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
        }

        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
        yield return ShowDialogBoxRoutine(false);
    }
}
