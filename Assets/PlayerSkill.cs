using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill : MonoBehaviour
{
    public PlayerMove playerMove;
    public bool CoolOn;
    
    public float timeStopDeltaTime()
    {
        float objectTimeScale = 0.2f;
        float customDeltaTime = objectTimeScale * Time.deltaTime;

        return customDeltaTime;

    }
    
    void timeStop()
    {
        if (CoolOn)
        {
            Time.timeScale = 0.2f;
            Time.fixedDeltaTime = 0.2f * Time.timeScale;
        }

    }
    
    
}
