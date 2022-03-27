using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public List<Cube> enemyCubes;

    private float _currentTime;
    private float _maxTime;


    private void Awake()
    {
        instance = this;
    }

    void Update()
    {
        CubeCheckTimer();
    }
    private void CubeCheckTimer()
    {
        _currentTime += Time.fixedDeltaTime;

        if (_currentTime > _maxTime)
        {
            // _maxIdleTime 초마다 실행되는 코드.
            _maxTime = 0.3f;

        }
        AllCubeChecker();
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

    
}
