using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BBSelectionManager : MonoBehaviour
{
    public int choiceNum;
    public List<string> selectionList;
    public GameObject barPrefab;
    public GameObject barCanvas;
    

    private KeyCode[] keyCodes = { KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3, KeyCode.Alpha4 };

    private void Awake()
    {
        DialogManager.instance.lastChoice = 0;
    }

    private void OnEnable()
    {
        selectionList = DialogManager.instance.currentChoice.lines;
        choiceNum = selectionList.Count;

        for (int i = 0; i < choiceNum; i++)
        {
            GameObject selectionBar = Instantiate(barPrefab);
            selectionBar.transform.SetParent(barCanvas.transform, true);

            selectionBar.GetComponent<BBChoiceBoxControl>().text_Ypos = 2f - (1.5f * i);

            selectionBar.transform.GetComponent<Text>().text = (i + 1) + ") " + selectionList[i];

            selectionBar.transform.GetComponent <BBChoiceBoxControl>().myNum = i;
        }
    }

    private void Update()
    {
        if (choiceNum == null) return;
        for (int i = 0; i < choiceNum; i++)
        {
            if (Input.GetKeyDown(keyCodes[i]))
            {
                DialogManager.instance.lastChoice = i;
            }
        }
    }
}
