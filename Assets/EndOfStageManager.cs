using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndOfStageManager : MonoBehaviour
{
    public int myStgNum;
    public bool blockOn = false;
    public GameObject endOfStages;

    private void Awake()
    {
        endOfStages = transform.parent.gameObject;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            BBGameManager.instance.ground.transform.GetChild(myStgNum).gameObject.SetActive(false);
            BBGameManager.instance.ground.transform.GetChild(myStgNum + 1).gameObject.SetActive(true);
            BBGameManager.instance.cams.transform.GetChild(myStgNum + 2).gameObject.SetActive(true);
            BBGameManager.instance.cams.transform.GetChild(myStgNum + 1).gameObject.SetActive(false);
            BBGameManager.instance.currentSceneNum = myStgNum;
            
            blockOn = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player" && endOfStages.transform.childCount > 1)
        {
            endOfStages.transform.GetChild(myStgNum + 1).gameObject.SetActive(true);
            Destroy(this);
        }
    }
}
