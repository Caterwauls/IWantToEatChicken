using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialog
{
    public List<string> lines = new List<string>();
}

public class DialogManager : MonoBehaviour
{
    public static DialogManager instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<DialogManager>();
            return _instance;
        }
    }

    private static DialogManager _instance;

    public TextAsset dialogFile;
    public TextAsset choiceFile;

    public GameObject dialogBox;
    public GameObject blurEffect;
    public GameObject playerSelectionMode;
    public CanvasScaler dialogBoxScaler;
    public GameObject promptEffect;

    public Dictionary<string, Dialog> dialogs = new Dictionary<string, Dialog>();
    public Dictionary<string, Dialog> choices = new Dictionary<string, Dialog>();

    public Dictionary<char, string> richTextAfterWord = new Dictionary<char, string>();

    public int lastChoice = 0;
    public bool didSelect = false;
    public Text dialogText;

    public float perCharacterDelay = 0.05f;
    public GameObject characterEffect;

    public Dialog currentChoice = null;

    public bool enableDialogBoxAnimation = true;

    private void Awake()
    {
        LoadFromTextAsset(dialogFile, dialogs);
        LoadFromTextAsset(choiceFile, choices);

        richTextAfterWord.Add('b', "</b>");
        richTextAfterWord.Add('i', "</i>");
        richTextAfterWord.Add('s', "</size>");
        richTextAfterWord.Add('c', "</color>");
    }

    private void LoadFromTextAsset(TextAsset asset, Dictionary<string, Dialog> dict)
    {
        string[] lines = asset.text.Split('\n');
        string dialogName = "";
        Dialog currentDialog = null;
        foreach (string rawLine in lines)
        {
            string line = rawLine.Trim();
            if (line == "") continue;
            if (line.StartsWith("#"))
            {
                if (currentDialog != null)
                    dict.Add(dialogName, currentDialog);
                dialogName = line.Substring(1);
                currentDialog = new Dialog();
                continue;
            }

            currentDialog.lines.Add(line);
        }

        dict.Add(dialogName, currentDialog);
    }
}
