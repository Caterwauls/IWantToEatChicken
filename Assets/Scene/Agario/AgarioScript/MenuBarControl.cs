using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuBarControl : MonoBehaviour
{
    private void OnEnable()
    {
        for (int i = 0; i < transform.GetChildCount(); i++)
        {
            if (i == 0)
            {
                transform.GetChild(0).gameObject.SetActive(true);
            }
            else
                transform.GetChild(i).gameObject.SetActive(false);
        }
    }
}
