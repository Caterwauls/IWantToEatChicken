using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ERUIPotionDisplay : MonoBehaviour
{
    public ERPlayerPotion potion;
    public Text countText;
    public GameObject[] objectsToActivate;

    private void FixedUpdate()
    {
        for (int i = 0; i < objectsToActivate.Length; i++)
        {
            objectsToActivate[i].SetActive(potion.currentPotion > 0);
        }

        countText.text = potion.currentPotion == 0 ? "" : potion.currentPotion.ToString();
    }
}
