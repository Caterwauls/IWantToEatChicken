using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using System.Linq;


public class AGGameManager : MonoBehaviour
{
    public static AGGameManager instance;
    public List<Cube> enemyCubes;
    public Cube myCube;
    public Text remainCubes;
    public UnityEvent onPlayerWin;
    public UnityEvent onPlayerDead;
    public GameObject[] audios;
    public GameObject playerDeadEffect;
    public Vector3 playerPos;
    public bool isPlayerDead = false;
    public bool isCanPlayerMove = false;
    public bool isCanUseUi = false;

    public Flow_AG flow;

    public Text leaderboard;


    private float lastTimeScale;
    private float _currentTime;
    private float _maxTime;



    private int remainCubeNum = 0;


    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        flow.StartFlow();
        audios = GameObject.FindGameObjectsWithTag("Audio");
        lastTimeScale = Time.timeScale;
    }

    void Update()
    {
        if (!flow.isDialogEnd) return;
        playerPos = myCube.transform.position;
        audioTimeControl();

        if (SceneManager.GetActiveScene().buildIndex == 1) return;

        AllCubeChecker();
        UpdateRanking();
        UpdateRemainCube();

        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }


        if (!myCube.gameObject.activeSelf && !isPlayerDead)
        {
            Flow_AG._deadNum++;
            isPlayerDead = true;
            onPlayerDead.Invoke();
        }

        if (enemyCubes.Count <= 0)
        {
            onPlayerWin.Invoke();

        }
    }
    public void PlayerDeadEffect()
    {
        var a = Instantiate(playerDeadEffect);
        a.transform.position = playerPos;
    }




    public void AllCubeChecker()
    {
        enemyCubes.Clear();
        CubeBehavior[] list = FindObjectsOfType<CubeBehavior>();
        if (list == null) return;
        PlayerMove playerCube = FindObjectOfType<PlayerMove>();



        for (int i = 0; i < list.Length; i++)
        {
            enemyCubes.Add(list[i].GetComponent<Cube>());
            if (myCube.CanEat(enemyCubes[i]) && enemyCubes[i] != null)
            {
                enemyCubes[i].GetComponent<Outline>().enabled = true;
            }
            else if (enemyCubes[i] == null)
            {
                continue;
            }


            else
                enemyCubes[i].GetComponent<Outline>().enabled = false;
        }


    }

    public void RestartGame()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void PlayerDie()
    {

        StartCoroutine(DeadTimer(5f));

    }

    IEnumerator DeadTimer(float TimerTime)
    {
        yield return new WaitForSeconds(TimerTime);
        RestartGame();
    }

    void UpdateRanking()
    {
        List<Cube> rankingCubeList = new List<Cube>();
        List<Cube> allCubes = enemyCubes.ToList();

        allCubes.Add(myCube);


        var sortAllCubes = allCubes.OrderByDescending(x => x.energy).ToList();

        leaderboard.text = "";
        for (int i = 0; i < 4; i++)
        {
            if (i + 1 > sortAllCubes.Count) break;
            rankingCubeList.Add(sortAllCubes[i]);
            leaderboard.text += $"{i + 1}. {rankingCubeList[i].gameObject.name}: " + string.Format("{0:0.0}", rankingCubeList[i].energy) + "\n";
        }

        rankingCubeList.Clear();
        sortAllCubes.Clear();

    }

    void UpdateRemainCube()
    {
        remainCubeNum = enemyCubes.Count;
        remainCubes.text = "남은 큐브 수: " + remainCubeNum;
    }

    

    void audioTimeControl()
    {

        if (Time.timeScale != lastTimeScale)
        {
            lastTimeScale = Time.timeScale;
            for (int i = 0; i < audios.Length; i++)
            {
                audios[i].GetComponent<AudioSource>().pitch = Time.timeScale;
            }
        }

    }
}
