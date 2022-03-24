using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    public Cube prefab;
    public int cubeCount = 30;
    public float minEnergy = 1;
    public float maxEnergy = 5;

    public float allowedDistance = 3f;

    private BoxCollider _box;

    private void Start()
    {
        _box = GetComponent<BoxCollider>();
    
        for (int i = 0; i < cubeCount; i++)
        {
            Cube newCube = Instantiate(prefab, GetRandomPoint(), Random.rotation);
            newCube.energy = Random.Range(minEnergy, maxEnergy);
            newCube.GetComponent<MeshRenderer>().material.color = Random.ColorHSV(0, 1, 1, 1, 0.75f, 1);
        }
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
