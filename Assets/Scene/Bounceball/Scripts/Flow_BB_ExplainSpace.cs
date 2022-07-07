using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flow_BB_ExplainSpace : Flow
{
    private static bool _didListen;

    public GameObject spaceToWideViewText;

    protected override IEnumerator FlowRoutine()
    {
        if (_didListen) yield break;
        BBPlayerMovement move = FindObjectOfType<BBPlayerMovement>();
        
        DialogManager.instance.enableDialogBoxAnimation = false;
        move.playerMoveOn = false;
        yield return new WaitForSeconds(1f);
        yield return PrintDialogRoutine("한치앞도");
        spaceToWideViewText.SetActive(true);
        yield return new WaitUntil(() => FindObjectOfType<BBCamControl>().wideCam.activeSelf);
        yield return new WaitForSeconds(2f);
        yield return PrintDialogRoutine("볼수있어");
        FindObjectOfType<BBCamControl>().wideCam.SetActive(false);
        spaceToWideViewText.SetActive(false);
        yield return new WaitForSeconds(1f);
        move.playerMoveOn = true;
        _didListen = true;
    }
    
    protected override IEnumerator PrintDialogRoutine(string dialogName)
    {
        DialogManager.instance.dialogBox.SetActive(true);
        yield return base.PrintDialogRoutine(dialogName);
        DialogManager.instance.dialogBox.SetActive(false);
    }
}