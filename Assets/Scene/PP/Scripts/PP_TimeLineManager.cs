using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PP_TimeLineManager : MonoBehaviour
{
    public PlayableDirector playerableDirector;
    public TimelineAsset timeline;
    public Text escapeMessageText;
    public GameObject escapeMessage;
    public SpacePressEffectOn pressEffect;

    IEnumerator TimeLineFlow()
    {
        escapeMessageText.text = "탈출하기";
        escapeMessage.SetActive(true);
        pressEffect.canPlayEffect = true;
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        yield return new WaitForSecondsRealtime(1f);
        escapeMessage.SetActive(false);
        pressEffect.canPlayEffect = false;
        TimelinePlay();
    }
    public void TimelinePlay()
    {
        playerableDirector.Play();
    }

    public void TimelineStop()
    {
        playerableDirector.Pause();
        StartCoroutine(TimeLineFlow());

    }

    public void LoadBballLoadingScene()
    {
        SceneManager.LoadScene("BBallLoadingScene");
    }

}
