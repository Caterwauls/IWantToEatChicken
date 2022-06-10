using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using UnityEngine.Rendering.PostProcessing;
public class PPGameManager : MonoBehaviour
{
    public Rigidbody player;
    public CanvasScaler dialogBoxSize;
    public GameObject playerCamera;
    public GameObject otherCamera;
    public GameObject otherPingPong;
    public Text dialogText;
    public List<string> answerChoice;
    public Transform ClosetChoice;
    public GameObject waitEnter;
    public bool flowRestart = false;
    public DialogList dialog;
    public GameObject systemMessage;
    
    

    public GameObject Blur;
    public GameObject playerSelectionRing;
    public int selectedAnswer;

    private GameObject _dialogBox;
    private Camera _mainCamera;



    public static PPGameManager instance = null;
    public static int answerNum;
    public static int resistCount;
    private void Awake()
    {
        _mainCamera = Camera.main;
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (instance != this)
                Destroy(this.gameObject);
        }
        _dialogBox = dialogBoxSize.gameObject;
    }
    void Start()
    {
        
        StartCoroutine(MessageFlow());

    }

    // Update is called once per frame
    void Update()
    {
        resistCount = player.GetComponent<PlayerControl>().resistNum;
    }


    IEnumerator MessageFlow()
    {
        Action answerFlowStart = () =>
        {
            _dialogBox.SetActive(false);
            Blur.SetActive(true);
            playerSelectionRing.SetActive(true);
        };

        Action answerFlowEnd = () =>
        {
            Blur.SetActive(false);
            playerSelectionRing.SetActive(false);
            _dialogBox.SetActive(true);
        };

        yield return new WaitForSeconds(1f);

        playerCamera.SetActive(true);

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
        yield return DialogBoxCotrol(true);

        yield return new WaitForSecondsRealtime(1.5f);

        yield return SystemMessageTyping(0);


        

        answerFlowStart();

        yield return new WaitUntil(() => flowRestart);

        answerFlowEnd();

        yield return SystemMessageTyping(1);

        answerFlowStart();

        yield return new WaitUntil(() => flowRestart);

        answerFlowEnd();

        yield return SystemMessageTyping(2);

        _dialogBox.SetActive(false);

        otherCamera.SetActive(true);


        Time.timeScale = 1;
        otherPingPong.SetActive(true);

        yield return new WaitForSecondsRealtime(4f);

        
        _dialogBox.SetActive(true);
        yield return SystemMessageTyping(3);
        _dialogBox.SetActive(false);
        otherCamera.SetActive(false);

        yield return new WaitForSecondsRealtime(2f);

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
        yield return new WaitForSecondsRealtime(1.5f);

        _dialogBox.SetActive(true);
        yield return SystemMessageTyping(4);
        _dialogBox.SetActive(false);
        Time.timeScale = 1;

        playerCamera.SetActive(false);

        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space)||Input.GetKeyDown(KeyCode.Return));
        
        systemMessage.SetActive(true);
        player.GetComponent<PlayerControl>().isCanResist = true;
        
        yield return new WaitUntil(() => resistCount >= 7);

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
        yield return new WaitForSecondsRealtime(1.5f);

        systemMessage.SetActive(false);
        _dialogBox.SetActive(true);
        yield return SystemMessageTyping(5);

        answerFlowStart();
        yield return new WaitUntil(() => flowRestart);
        answerFlowEnd();

        yield return SystemMessageTyping(6);

        answerFlowStart();
        yield return new WaitUntil(() => flowRestart);
        answerFlowEnd();

        if(selectedAnswer == 0)
        {
            yield return SystemMessageTyping(8);
        }
        else if(selectedAnswer == 1)
        {
            yield return SystemMessageTyping(7);
        }



    }

    IEnumerator DialogBoxCotrol(bool isShow)
    {
        while (true)
        {
            if (isShow && dialogBoxSize.scaleFactor < 1)
            {
                dialogBoxSize.scaleFactor += 0.08f;
                yield return new WaitForSecondsRealtime(0.01f);
            }

            else if (!isShow && dialogBoxSize.scaleFactor > 0.01f)
            {
                dialogBoxSize.scaleFactor -= 0.1f;
                yield return new WaitForSecondsRealtime(0.01f);
            }

            else if (dialogBoxSize.scaleFactor >= 1 || dialogBoxSize.scaleFactor <= 0.01f)
            {
                break;
            }
        }
    }

    public void AnswerList()
    {
        answerChoice.Clear();

        for (int i = 0; i < dialog.ANum[answerNum].AList.Length; i++)
        {
            answerChoice.Add(dialog.ANum[answerNum].AList[i]);
        }
    }

    IEnumerator SystemMessageTyping(int dialogNum)
    {
        flowRestart = false;
        waitEnter.SetActive(false);

        answerNum = dialogNum;
        AnswerList();

        
        for(int i = 0; i < dialog.DNum[dialogNum].DList.Length; i++)
        {
            dialogText.text = "";
            string text = dialog.DNum[dialogNum].DList[i];
            for (int j = 0; j < text.Length; j++)
            {
                dialogText.text += text[j];
                yield return new WaitForSecondsRealtime(0.1f);
            }
            yield return new WaitForSecondsRealtime(0.3f);

            dialogText.text += "_";
            waitEnter.SetActive(true);

            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
        }

        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        yield break;
    }
    
}
