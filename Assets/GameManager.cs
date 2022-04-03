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
    public UnityEvent onPlayerWin;


    private float _currentTime;
    private float _maxTime;

    private int remainCubeNum = 0;


    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {

    }

    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1) return;
        AllCubeChecker();
        updateRemainCube();
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
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
        Cube myCube = FindObjectOfType<PlayerMove>().GetComponent<Cube>();


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

        scene1Camera.gameObject.SetActive(false);
        SceneManager.LoadScene(1, LoadSceneMode.Additive);
        yield break;
        yield return new WaitForSeconds(1f);

        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(1));

        yield break;
    }


}
