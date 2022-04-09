using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerS2 : MonoBehaviour
{
    public Transform bossTransform;
    public Transform playerTransform;

    [SerializeField] private Texture2D cursorImg;


    void Start()
    {
        
    }

    
    void Update()
    {
        ChangeCursor();
        Cursor.lockState = CursorLockMode.Confined;
        float distance = Vector3.Distance(bossTransform.position, playerTransform.position); //181 최대 거리, 50 최소거리, 50~60, 60~90, 90~ 130, 130~180 으로 패턴 만들면 될듯.
        
    }

    void ChangeCursor()
    {
        Cursor.SetCursor(cursorImg, Vector2.zero, CursorMode.ForceSoftware);
    }
}
