using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flow : MonoBehaviour
{
    public int lastChoice
    {
        get
        {
            return DialogManager.instance.lastChoice;
        }
    }

    public void StartFlow()
    {
        StartCoroutine(FlowRoutine());
    }

    protected virtual IEnumerator FlowRoutine()
    {
        yield break;
    }

    private void StartAnswerFlow()
    {
        DialogManager.instance.dialogBox.SetActive(false);
        DialogManager.instance.blurEffect.SetActive(true);
        DialogManager.instance.playerSelectionMode.SetActive(true);
    }

    private void EndAnswerFlow()
    {
        DialogManager.instance.dialogBox.SetActive(true);
        DialogManager.instance.blurEffect.SetActive(false);
        DialogManager.instance.playerSelectionMode.SetActive(false);
    }

    protected IEnumerator StopTimeAnimatedRoutine()
    {
        if (!DialogManager.instance.enableDialogBoxAnimation)
        {
            yield break;
        }
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
        if (!DialogManager.instance.enableDialogBoxAnimation)
        {
            if (isShow && DialogManager.instance.dialogBoxScaler.scaleFactor < 1)
            {
                DialogManager.instance.dialogBoxScaler.scaleFactor = 1;
                yield break;
            }

            else if (!isShow && DialogManager.instance.dialogBoxScaler.scaleFactor > 0.01f)
            {
                DialogManager.instance.dialogBoxScaler.scaleFactor = 0.01f;
                yield break;
            }

            else if (DialogManager.instance.dialogBoxScaler.scaleFactor >= 1 ||
                     DialogManager.instance.dialogBoxScaler.scaleFactor <= 0.01f)
            {
                yield break;
            }
        }

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

    protected virtual IEnumerator AskChoiceRoutine(string selectionName)
    {
        DialogManager.instance.currentChoice = DialogManager.instance.choices[selectionName];
        DialogManager.instance.didSelect = false;
        yield return new WaitForSecondsRealtime(1f);
        StartAnswerFlow();
        yield return new WaitUntil(() => DialogManager.instance.didSelect);
        EndAnswerFlow();
    }

    private bool shouldSkip = false;
    protected virtual IEnumerator PrintDialogRoutine(string dialogName)
    {
        DialogManager.instance.dialogText.text = "";

        yield return ShowDialogBoxRoutine(true);

        Dialog dialog = DialogManager.instance.dialogs[dialogName];

        for (int i = 0; i < dialog.lines.Count; i++)
        {
            DialogManager.instance.promptEffect.SetActive(false);
            DialogManager.instance.dialogText.text = "";
            string text = dialog.lines[i];

            shouldSkip = false;
            Coroutine checkRoutine = StartCoroutine(PrintDialogCheckForSkipRoutine());
            List<string> currentActiveSuffixes = new List<string>();
            string currentTypedText = "";
            for (int j = 0; j < text.Length; j++)
            {
                // Tag를 만났으면?
                if (text[j] == '<')
                {
                    // 그 태그가 Closing Tag라면?
                    if (text[j + 1] == '/')
                    {
                        // Closing Tag를 완전히 스킵해서 > 다음까지 간다.
                        while (text[j] != '>')
                        {
                            currentTypedText += text[j];
                            j++;
                        }
                        currentTypedText += text[j];
                        currentActiveSuffixes.RemoveAt(0);
                    }
                    else // Opening Tag라면?
                    {
                        j++;
                        char currrentTagFirstChar = text[j];

                        DialogManager.instance.richTextAfterWord.TryGetValue(currrentTagFirstChar, out string tag);
                        currentActiveSuffixes.Insert(0, tag);

                        // Opening Tag를 전부 집어넣는다.
                        currentTypedText += '<';
                        while (text[j] != '>')
                        {
                            currentTypedText += text[j];
                            j++;
                        }
                        currentTypedText += text[j];
                    }
                }
                else // 태그를 만난게 아니고 그냥 일반 글자라면?
                {
                    currentTypedText += text[j];
                }

                if (shouldSkip)
                {
                    shouldSkip = false;
                    DialogManager.instance.dialogText.text = text;
                    break;
                }

                string combinedSuffixes = "";
                for (int k = 0; k < currentActiveSuffixes.Count; k++)
                {
                    combinedSuffixes += currentActiveSuffixes[k];
                }

                // 텍스트를 여기서 업데이트.
                DialogManager.instance.dialogText.text = currentTypedText + combinedSuffixes;
                if (DialogManager.instance.characterEffect != null) Instantiate(DialogManager.instance.characterEffect);
                yield return new WaitForSecondsRealtime(DialogManager.instance.perCharacterDelay);
            }
            StopCoroutine(checkRoutine);
            yield return new WaitForSecondsRealtime(0.3f);

            DialogManager.instance.dialogText.text += "_";
            DialogManager.instance.promptEffect.SetActive(true);

            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
        }
        DialogManager.instance.promptEffect.SetActive(false);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
        yield return ShowDialogBoxRoutine(false);
    }

    private IEnumerator PrintDialogCheckForSkipRoutine()
    {
        while (true)
        {
            yield return null;
            if (Input.GetKeyDown(KeyCode.Return))
                shouldSkip = true;
        }
    }
}
