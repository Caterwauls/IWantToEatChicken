using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionBar : MonoBehaviour
{
    public float dis;
    public Transform closetChoice;
    public int myNum;

    private void Awake()
    {
        
    }
    void Start()
    {
        closetChoice = PPGameManager.instance.ClosetChoice;
        
    }

    // Update is called once per frame
    void Update()
    {
        dis = Vector3.Magnitude(transform.position - Camera.main.WorldToScreenPoint(closetChoice.position));
        if (dis <= 110)
        {
            transform.GetComponent<Image>().color = new Color(1, 1, 1, 217 / 255f);
            if (Input.GetKeyDown(KeyCode.Return))
            {
                PPGameManager.instance.selectedAnswer = myNum;
                PPGameManager.instance.flowRestart = true;
                Destroy(gameObject);
            }
        }
        else
        {
            transform.GetComponent<Image>().color = new Color(101 / 255f, 101 / 255f, 101 / 255f, 130 / 255f);
            if (Input.GetKeyDown(KeyCode.Return))
            {
                Destroy(gameObject);
            }
                
        }
        
    }
}
