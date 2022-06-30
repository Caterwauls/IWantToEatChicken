using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BBSpawnOrb : MonoBehaviour
{
    public GameObject abilityOrbPrefab;

    private float timer;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (timer < 1)
        {
            timer += Time.fixedDeltaTime;
            return;
        }
        timer = 0;
        if (transform.childCount < 1 && BBGameManager.instance.currentSceneNum == 1)
        {
            GameObject abilityOrb = Instantiate(abilityOrbPrefab, transform.position, Quaternion.identity);
            abilityOrb.transform.parent = gameObject.transform;
        }
    }
}
