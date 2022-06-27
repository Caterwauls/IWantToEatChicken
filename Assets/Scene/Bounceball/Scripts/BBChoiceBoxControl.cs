using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class BBChoiceBoxControl : MonoBehaviour
{
    public Transform playerChoicePos;
    public float text_Xpos = -1.5f;
    public float text_Ypos;
    public int myNum;
    public GameObject underLine;

    private Camera _mainCam;


    private void Awake()
    {
        _mainCam = Camera.main;
    }

    private void Start()
    {
        playerChoicePos = FindObjectOfType<BBSelectionManager>().gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Camera.main.WorldToScreenPoint(playerChoicePos.position + new Vector3(-text_Xpos, text_Ypos));

        if (myNum == DialogManager.instance.lastChoice)
        {
            underLine.SetActive(true);
            if (Input.GetKeyDown(KeyCode.Return))
            {
                DialogManager.instance.didSelect = true;
                Destroy(gameObject);
            }
        }
        else
        {
            underLine.SetActive(false);
            if (Input.GetKeyDown(KeyCode.Return))
            {
                Destroy(gameObject);
            }
        }
    }
}
