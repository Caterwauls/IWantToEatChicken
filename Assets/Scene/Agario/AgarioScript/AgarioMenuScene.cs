using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AgarioMenuScene : MonoBehaviour
{
    public GameObject inputText;

    private string playerName = null;

    [SerializeField] private Texture2D cursorImg;

    private void Awake()
    {
        PlayerPrefs.SetString("AgarioPlayerName", "");
        playerName = inputText.GetComponent<Text>().text;
    }

    private void Update()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.SetCursor(cursorImg, Vector2.zero, CursorMode.ForceSoftware);
    }
    //마우스
    public void InputName()
    {
        playerName = inputText.GetComponent<Text>().text;
        PlayerPrefs.SetString("AgarioPlayerName", playerName);
        inputText.GetComponent<Text>().text = "";
    }

    public void goNextSceneOnClick()
    {
        InputName();
        if (PlayerPrefs.GetString("AgarioPlayerName") != "")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        
    }
}
