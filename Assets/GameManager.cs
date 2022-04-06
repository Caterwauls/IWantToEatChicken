using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;
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

    private float lastTimeScale;
    private float _currentTime;
    private float _maxTime;

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

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        if (SceneManager.GetActiveScene().buildIndex == 1) return;

        AllCubeChecker();
        updateRemainCube();

        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }

        
        if(myCube == null)
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
        NPCSkill[] list = FindObjectsOfType<NPCSkill>();
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

    void updateRemainCube()
    {
        remainCubeNum = enemyCubes.Count;
        remainCubes.text = "Cubes left: " + remainCubeNum;
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
            for(int i=0; i<audios.Length; i++)
            {
                audios[i].GetComponent<AudioSource>().pitch = Time.timeScale;
            }
        }

    }


}
