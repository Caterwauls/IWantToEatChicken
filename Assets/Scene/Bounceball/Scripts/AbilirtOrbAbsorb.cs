using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilirtOrbAbsorb : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            BBGameManager.instance.abillityRoutineOn = true;
            Destroy(this.gameObject);
        }
    }
}
