using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public int currentSceneNum;
    // Start is called before the first frame update
    void Start()
    {
        flow.StartFlow();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
}
