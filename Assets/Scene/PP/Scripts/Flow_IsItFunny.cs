using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = System.Random;

public class Flow_IsItFunny : Flow
{
    public float timeBeforeStart = 10f;
    public float explodeVelocity = 10f;
    public float explodeRot = 800f;
    
    public PPBreakable leftBar;
    public PPBreakable rightBar;
    public PPBreakable box;

    public GameObject resistMessage;
    public GameObject moveMessage;
    
    public GameObject otherCamera;
    public GameObject otherPingPong;
    public GameObject player;

    public Transform playerJailPos;

    public string nextSceneName;

    protected override IEnumerator FlowRoutine()
    {
        PPPlayer pc = player.GetComponent<PPPlayer>();
        pc.restraintCount = 10;
        yield return new WaitForSecondsRealtime(timeBeforeStart);
        resistMessage.SetActive(true);
        pc.canResist = true;
        yield return new WaitUntil(() => pc.restraintCount == 0);
        resistMessage.SetActive(false);
        moveMessage.SetActive(true);
        yield return new WaitUntil(() => leftBar.curHP == 0);
        yield return new WaitUntil(() => rightBar.curHP == 0);
        moveMessage.SetActive(false);
        yield return new WaitForSeconds(4f);
        yield return PrintDialogRoutine("뭐해");
        otherCamera.SetActive(true);
        otherPingPong.SetActive(true);
        yield return new WaitForSecondsRealtime(4f);
        otherCamera.SetActive(false);
        yield return new WaitForSecondsRealtime(1.75f);
        leftBar.ReturnToOriginal();
        rightBar.ReturnToOriginal();
        yield return new WaitForSecondsRealtime(1f);
        yield return PrintDialogRoutine("역할");
        pc.canResist = false;
        pc.restraintCount = 15;
        yield return new WaitForSeconds(7f);
        pc.canResist = true;
        resistMessage.SetActive(true);
        yield return new WaitUntil(() => pc.restraintCount == 0);
        resistMessage.SetActive(false);
        yield return new WaitUntil(() => leftBar.curHP == 0);
        yield return new WaitUntil(() => rightBar.curHP == 0);
        yield return new WaitForSeconds(4f);
        pc.transform.position = playerJailPos.position;
        box.gameObject.SetActive(true);
        pc.canPlayerMove = false;
        pc.GetComponent<Rigidbody>().velocity = Vector3.zero;
        pc.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        yield return new WaitForSeconds(1.5f);
        yield return PrintDialogRoutine("마지막");
        pc.canPlayerMove = true;
        yield return new WaitUntil(() => box.curHP == 0);
        var rbs = FindObjectsOfType<Rigidbody>();
        foreach (var rb in rbs)
        {
            rb.isKinematic = false;
            rb.useGravity = true;
            rb.velocity = UnityEngine.Random.onUnitSphere * explodeVelocity;
            rb.angularVelocity = Vector3.forward * UnityEngine.Random.Range(-explodeRot, explodeRot);
        }
        yield return new WaitForSeconds(5.5f);
        SceneManager.LoadScene(nextSceneName);
    }
}
