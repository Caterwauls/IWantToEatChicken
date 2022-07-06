﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    public Cube prefab;
    public Cube foodCube;
    public int cubeCount = 30;
    public int foodCount = 30;
    public float minEnergy = 5;
    public float maxEnergy = 10;
    public float foodMin = 0.2f;
    public float foodMax = 1f;
    public GameObject cubeNameManager;
    public List<Cube> enemyCubeList = new List<Cube>();

    public string[] cubeNames =
    {
        "오헤부너", "무한복귀","ZI존아델S22","검찢","클링위즈","흰뿔도마뱀","MADGECKO","제일약한전사","버프귤","Lunaris",
        "쁘쁘띠따","system준몽","루시케","TT소년","토익잘보자","새우고로케","AUG로어그로","배고프다","훔","아악", "염소"
    };

    public float allowedDistance = 3f;

    private BoxCollider _box;

    private void Awake()
    {
    }
    private void Start()
    {
        _box = GetComponent<BoxCollider>();

        for (int i = 0; i < cubeCount; i++)
        {
            Cube newCube = Instantiate(prefab, GetRandomPoint(), Random.rotation);
            enemyCubeList.Add(newCube);
            newCube.cubeName = cubeNames[i];
            newCube.energy = Random.Range(minEnergy, maxEnergy);
            newCube.rangeObject = gameObject;
            newCube.rangeCollider = GetComponent<BoxCollider>();
            newCube.GetComponent<MeshRenderer>().material.color = Random.ColorHSV(0, 1, 1, 1, 0.75f, 1);
        }

        for (int j = 0; j < foodCount; j++)
        {
            Cube newFood = Instantiate(foodCube, GetRandomPoint(), Random.rotation);
            newFood.energy = Random.Range(foodMin, foodMax);
            newFood.cubeName = "food";
            newFood.GetComponent<MeshRenderer>().material.color = Random.ColorHSV(0, 1, 0.1f, 0.5f, 0.75f, 1);
        }

        cubeNameManager.SetActive(true);
    }

    private Vector3 GetRandomPoint()
    {
        Vector3 min = _box.bounds.min;
        Vector3 max = _box.bounds.max;

        Vector3 randomPoint = Vector3.zero;
        for (int tries = 1; tries <= 100; tries++)
        {
            randomPoint = new Vector3(Random.Range(min.x, max.x), Random.Range(min.y, max.y), Random.Range(min.z, max.z));

            Collider[] cols = Physics.OverlapSphere(randomPoint, allowedDistance);
            bool isInvalid = false;
            foreach (Collider col in cols)
            {
                if (col.GetComponent<Cube>() != null)
                {
                    isInvalid = true;
                    break;
                }
            }
            if (isInvalid)
            {
                continue;
            }
            break;
        }

        return randomPoint;
    }

    private void Update()
    {

    }
}
