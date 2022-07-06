using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionBar : MonoBehaviour
{
    public int myNum;
    public Color selectedColor = new Color(1, 1, 1, 217 / 255f);
    public Color dimColor = new Color(101 / 255f, 101 / 255f, 101 / 255f, 130 / 255f);
    
    private void Update()
    {
        if (myNum == DialogManager.instance.lastChoice)
        {
            transform.GetComponent<Image>().color = selectedColor;
            if (Input.GetKeyDown(KeyCode.Return))
            {
                DialogManager.instance.didSelect = true;
                Destroy(gameObject);
            }
        }
        else
        {
            transform.GetComponent<Image>().color = dimColor;
            if (Input.GetKeyDown(KeyCode.Return))
            {
                Destroy(gameObject);
            }
        }
    }
}
