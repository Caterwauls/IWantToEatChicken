using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CubeNameManager : MonoBehaviour
{
    public GameObject textPrefab;
    public CubeSpawner cubeSpawner;

    private Cube _target;

    private void Start()
    {
        var cubes = cubeSpawner.enemyCubeList;
        cubes.Add(AGGameManager.instance.myCube);

        foreach(Cube cube in cubes)
        {
            var text = Instantiate(textPrefab);
            text.transform.SetParent(transform);
            text.GetComponent<CubeNameText>().target = cube;
            text.GetComponent<Text>().text = cube.cubeName;
            text.GetComponent<Text>().color = cube.GetComponent<MeshRenderer>().material.color;
        }
        

    }
}
