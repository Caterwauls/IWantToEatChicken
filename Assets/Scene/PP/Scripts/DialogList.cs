using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogNum
{
    [TextArea]
    public string[] DList;

}

[System.Serializable]
public class AnswerNum
{
    [TextArea]
    public string[] AList;
}
public class DialogList : MonoBehaviour
{
    public DialogNum[] DNum;
    public AnswerNum[] ANum;
}
