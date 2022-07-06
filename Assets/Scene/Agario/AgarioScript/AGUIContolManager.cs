using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AGUIContolManager : MonoBehaviour
{
    public GameObject blur;
    public GameObject inGameUI;
    public GameObject menus;


    [SerializeField] private Texture2D cursorImg;

    private void Update()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.SetCursor(cursorImg, Vector2.zero, CursorMode.ForceSoftware);

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == 0)
            {
                blur.SetActive(false);
                inGameUI.SetActive(true);
                menus.SetActive(false);
                Time.timeScale = 1;



            }
            else
            {
                blur.SetActive(true);
                inGameUI.SetActive(false);
                menus.SetActive(true);
                Time.timeScale = 0;
            }


        }
    }

    public void OnClickResume()
    {
        blur.SetActive(false);
        inGameUI.SetActive(true);
        menus.SetActive(false);
        Time.timeScale = 1;
    }

    public void OnClickQuit()
    {
        Application.Quit();
    }

    public void OnClickGiveUp()
    {
        SceneManager.LoadScene(10);
    }
}
