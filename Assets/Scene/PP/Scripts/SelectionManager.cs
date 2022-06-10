using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionManager : MonoBehaviour
{
    public int choiceNum;
    public List<string> selectionList;
    public GameObject barPrefab;
    public GameObject barCanvas;
    public GameObject player;
    public Transform barPos;

    private Vector3 _barVec;
    private Vector3 _playerVec;
    private int _barAngle;
    private int _startRot = 45;
    private List<int> _possibleAngle;
    private KeyCode[] keyCodes = { KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3, KeyCode.Alpha4 };

    private void Awake()
    {
        
    }

    private void OnEnable()
    {
        _playerVec = Camera.main.WorldToScreenPoint(player.transform.position);
        _barVec = (Camera.main.WorldToScreenPoint(barPos.position) - _playerVec);

        selectionList = PPGameManager.instance.answerChoice;
        choiceNum = selectionList.Count;
        _barAngle = 360 / choiceNum;
        _possibleAngle = new List<int>();

        for (int i = 0; i < choiceNum; i++)
        {
            _possibleAngle.Add(-135 + _barAngle * i);

            GameObject selectionBar = Instantiate(barPrefab) as GameObject;
            selectionBar.transform.SetParent(barCanvas.transform, true);

            var quaternion = Quaternion.Euler(new Vector3(0, 0, _possibleAngle[i] - 135));
            selectionBar.transform.position = quaternion * _barVec + _playerVec; 

            Vector3 selectionBarPos = selectionBar.transform.position;
            selectionBar.transform.GetComponentInChildren<Text>().text = (i + 1)  + ") " + selectionList[i];

            selectionBar.transform.GetComponent<SelectionBar>().myNum = i;
        }

        

    }

    private void Update()
    {
        if (_possibleAngle == null) return;
        for(int i = 0; i < _possibleAngle.Count; i++)
        {
            if (Input.GetKeyDown(keyCodes[i]))
            {
                transform.rotation = Quaternion.Euler( new Vector3(0, 0, _possibleAngle[i]));

            }
        }
    }
}
