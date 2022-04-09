using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnergyToHp : MonoBehaviour
{
    public float energyToHp;
    public Cube playerCube;
    

    public void bringEnergy()
    {
        energyToHp = playerCube.energy;
        
    }
    
}
