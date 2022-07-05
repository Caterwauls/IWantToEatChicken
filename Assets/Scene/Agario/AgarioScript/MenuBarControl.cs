using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuBarControl : MonoBehaviour
{
    public int menuBarNum = 0;
    public GameObject[] menubars = new GameObject[4];

    private void Start()
    {
        StartCoroutine(menuBarRoutine());
        

    }

    IEnumerator menuBarRoutine()
    {
        while (true)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (menuBarNum != 0)
                {
                    menuBarNum++;
                }

            }
            else if(Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                if(menuBarNum != 3)
                {
                    menuBarNum--;
                }
            }
            EventSystem.current.SetSelectedGameObject(menubars[menuBarNum].gameObject);

            if (Input.GetKeyDown(KeyCode.Return))
            {
                menubars[menuBarNum].GetComponent<Button>().onClick.Invoke();
            }

            yield return new WaitForSecondsRealtime(0.1f);
        }
    }
}
