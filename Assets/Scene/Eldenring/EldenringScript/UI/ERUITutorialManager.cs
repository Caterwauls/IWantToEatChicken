using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ERUITutorialManager : MonoBehaviour
{
    private static List<string> _shownTutorials = new List<string>();

    public ERPlayer player;
    public GameObject movement;
    public GameObject savePoint;
    public GameObject combat;
    public GameObject hurt;

    private void Start()
    {
        ERGameManager.instance.onEnemyTargetAcquisition += () =>
        {
            if (_shownTutorials.Contains("combat")) return;
            _shownTutorials.Add("combat");
            combat.SetActive(true);
        };

        ERGameManager.instance.onSavePointUse += () =>
        {
            if (_shownTutorials.Contains("savePoint")) return;
            _shownTutorials.Add("savePoint");
            savePoint.SetActive(true);
        };
        
        StartCoroutine(Routine());

        IEnumerator Routine()
        {
            if (_shownTutorials.Contains("movement")) yield break;
            _shownTutorials.Add("movement");
            yield return new WaitForSeconds(1.5f);
            movement.SetActive(true);
        }
    }

    private void Update()
    {
        if (player.health < player.maxHealth * 0.6f)
        {
            if (_shownTutorials.Contains("hurt")) return;
            _shownTutorials.Add("hurt");
            hurt.SetActive(true);
        }
    }
}
