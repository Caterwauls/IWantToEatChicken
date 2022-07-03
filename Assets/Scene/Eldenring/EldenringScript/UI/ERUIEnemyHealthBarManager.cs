using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ERUIEnemyHealthBarManager : MonoBehaviour
{
    public ERUIEnemyHealthBar barPrefab;
    private void Awake()
    {
        ERGameManager.instance.onEnemySpawn += (e) =>
        {
            Instantiate(barPrefab, transform).target = e;
        };
    }
}
