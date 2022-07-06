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

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        if (flow != null) 
            flow.StartFlow();
    }
}
