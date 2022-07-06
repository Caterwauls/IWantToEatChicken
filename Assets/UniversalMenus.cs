using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UniversalMenus : MonoBehaviour
{
    public GameObject menuBase;
    private bool _isOpen;
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // 열려있지 않지만 TimeScale이 정상과 다르면 무시.
            if (!_isOpen && Time.timeScale < 0.9f) return;

            ToggleMenu();
        }
    }

    public void ToggleMenu()
    {
        _isOpen = !_isOpen;
        if (_isOpen)
        {
            menuBase.SetActive(!menuBase.activeSelf);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(false);
            }
        }
        Time.timeScale = !_isOpen ? 1f : 0f;
    }

    public void GiveUp()
    {
        SceneManager.LoadScene("PPScene");
        Time.timeScale = 1f;
    }

    public void Exit()
    {
        Application.Quit();
    }
}
