using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ERGameManager : MonoBehaviour
{
    public static ERGameManager instance
    {
        get
        {
            if (_instance == null) _instance = FindObjectOfType<ERGameManager>();
            return _instance;
        }
    }
    private static ERGameManager _instance;

    public static List<string> discoveredZones = new List<string>();
    public static Vector3 savePosition = Vector3.zero;

    public Action<EREnemy> onEnemySpawn;
    public Action<EREnemy> onEnemyDestroy;

    
    public Transform defaultSavePosition;
    public ERPlayer player;


    private void Awake()
    {
        if (savePosition == Vector3.zero) 
            savePosition = defaultSavePosition.position;
    }

    private void Start()
    {
        StartCoroutine(GameRoutine());
        IEnumerator GameRoutine()
        {
            player.transform.position = savePosition;
            ERUIManager.instance.FadeIn();
            yield break;
        }
    }

    public void EnterZone(string zoneName)
    {
        if (discoveredZones.Contains(zoneName)) return;
        discoveredZones.Add(zoneName);
        ERUIManager.instance.ShowNewZoneFoundMessage(zoneName);
    }
}
