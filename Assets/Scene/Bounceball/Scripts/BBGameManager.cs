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
   
    public int coolTime = 5;
    public GameObject abilityUI;
    public bool abillityRoutineOn = false;
    public bool abilityEnd = false;
    public bool canUseAbility = false;
    
    void Start()
    {
        flow.StartFlow();
        
    }

    
    void Update()
    {
        if (abillityRoutineOn)
        {
            abillityRoutineOn = false;
            StartCoroutine(PlayerAbilityOn());
        }
    }
    IEnumerator PlayerAbilityOn()
    {
        canUseAbility = true;
        abilityUI.SetActive(true);
        while(true)
        {
            if(!canUseAbility || abilityEnd)
            {
                canUseAbility = false;
                abilityEnd = true;
                abilityUI.GetComponent<RawImage>().color = Color.white;
                break;
            }
            yield return null;
            
        }
        yield break;
    }


}
