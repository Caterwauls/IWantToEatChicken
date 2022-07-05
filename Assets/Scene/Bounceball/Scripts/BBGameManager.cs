using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BBGameManager : MonoBehaviour
{
    public static BBGameManager instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<BBGameManager>();
            return _instance;
        }
    }
    private static BBGameManager _instance;

    public Flow flow;
    public Transform ground;
    public Transform cams;
    public int currentSceneNum = 0;
    public BBPlayer player;
    public int deadNum;
    public int currentStg;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        flow.StartFlow();
        if (!PlayerPrefs.HasKey("deadNum"))
        {
            PlayerPrefs.SetInt("deadNum", 0);
        }
        deadNum = PlayerPrefs.GetInt("deadNum");

        currentStg = FindObjectOfType<Flow>().name[11] - 48;
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.DeleteAll();
    }


}
