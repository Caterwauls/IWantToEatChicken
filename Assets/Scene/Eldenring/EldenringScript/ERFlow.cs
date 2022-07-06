using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ERFlow : Flow
{
    public AudioSource interactAudio;
    public CanvasGroup pressAnyButtonGroup;
    public CanvasGroup fadeGroup;
    public string nextSceneName;
    
    protected override IEnumerator FlowRoutine()
    {
        yield return PrintDialogRoutine("어느덧마지막");
        yield return AskChoiceRoutine("계속");
        if (lastChoice == 0)
            yield return PrintDialogRoutine("계속YES");
        else
            yield return PrintDialogRoutine("계속NO");

        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => Input.anyKeyDown);
        
        interactAudio.Play();
        BGMManager.instance.desiredClip = null;
        for (float t = 0; t < 1; t += Time.deltaTime * 2f)
        {
            pressAnyButtonGroup.alpha = 1 - t;
            yield return null;
        }
        for (float t = 0; t < 1; t += Time.deltaTime / 2f)
        {
            fadeGroup.alpha = t;
            yield return null;
        }

        fadeGroup.alpha = 1;
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(nextSceneName);
    }

    protected override IEnumerator ShowDialogBoxRoutine(bool isShow)
    {
        DialogManager.instance.dialogBox.SetActive(isShow);
        yield return new WaitForSeconds(0.5f);
    }
}
