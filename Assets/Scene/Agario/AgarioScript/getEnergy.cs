using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class getEnergy : MonoBehaviour
{
    public Cube playerCube;

    private Text energyText;

    private void Awake()
    {
        energyText = GetComponent<Text>();
    }

    private void Update()
    {
        energyText.text = "HP = " + string.Format("{0:0.#}", playerCube.energy);
    }
}
