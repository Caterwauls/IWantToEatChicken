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
    public Camera scene1Camera;
    public Text remainCubes;
    public TextAlpha textAlpha;
    public UnityEvent onPlayerWin;
    public UnityEvent WinTextEnd;
    public UnityEvent onPlayerDead;
    public bool isCutScenePlaying;
    public GameObject[] audios;

    public Text leaderboard;


    private float lastTimeScale;
    private float _currentTime;
    private float _maxTime;

    [SerializeField] private Texture2D cursorImg;

    private int remainCubeNum = 0;


    private void Awake()
    {
        instance = this;
        isCutScenePlaying = false;
    }

    private void Start()
    {
        audios = GameObject.FindGameObjectsWithTag("Audio");
        lastTimeScale = Time.timeScale;
    }

    void Update()
    {
        audioTimeControl();

        if (Input.GetKeyDown(KeyCode.Escape) && !isCutScenePlaying)
        {
            if (Time.timeScale == 0) Time.timeScale = 1;
            else Time.timeScale = 0;
        }


        Cursor.lockState = CursorLockMode.Confined;
        Cursor.SetCursor(cursorImg, Vector2.zero, CursorMode.ForceSoftware);

        if (SceneManager.GetActiveScene().buildIndex == 1) return;

        AllCubeChecker();
        UpdateRanking();
        UpdateRemainCube();

        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }


        if (myCube != myCube.gameObject.activeSelf)
        {
            onPlayerDead.Invoke();
        }

        if (enemyCubes.Count <= 0)
        {
            onPlayerWin.Invoke();
            StartCoroutine(WinTimer());
            return;

        }
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
                enemyCubes[i].GetComponent<Outline>().enabled =false;
        }


    }

    public void RestartGame()
    {

        SceneManager.LoadScene(0);
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

    IEnumerator WinTimer()
    {
        //myCube.enabled = false;
        //myCube.GetComponent<PlayerSkill>().enabled = false;



        yield return new WaitForSeconds(10f);

        //scene1Camera.gameObject.SetActive(false);
        //SceneManager.LoadScene(1, LoadSceneMode.Additive);
        //yield break;
        //yield return new WaitForSeconds(1f);

        //SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(1));
        WinTextEnd.Invoke();
        isCutScenePlaying = true;

        yield break;
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
