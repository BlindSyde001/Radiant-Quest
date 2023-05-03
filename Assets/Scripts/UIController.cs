using System.Collections;
using System.Collections.Generic;
using System;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public static UIController Instance; // Singleton instance
    public float charPerSecond;
    

    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private TextMeshProUGUI dialogueName;
    
    private static int index;
    private static List<string> dialogue = new List<string>{};

    private void Awake()
    {
        // Create or destroy duplicate instances of GameManager
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist the GameManager across scene loads
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        dialogueText.text = "";
        dialogueName.text = "";
    }

    // DIALOGUES
    // ---------
    public bool isDialogueActive() {
        return dialoguePanel.activeInHierarchy;
    }

    public string GetDialogueText() {
        return dialogueText.text;
    }

    public void DialogueSetActive(bool isActive) {
        dialoguePanel.SetActive(isActive);
    }

    public void StopDialogue() {
        ZeroText();
        Instance.DialogueSetActive(false);
    }

    // Starts a new dialogue on the scene and configures the name and list of lines for the dialogue
    public void StartDialogue(string dialogueName, object dialogue_) {
        ZeroText();
        Instance.SetDialogueName(dialogueName);
        
        if (dialogue_ is List<string>)
        {
            List<string> dialogueList = dialogue_ as List<string>;
            dialogue = dialogueList;
        }
        else if (dialogue_ is string)
        {
            string dialogueString = dialogue_ as string;
            dialogue = new List<string>{dialogueString};
        }
        else
        {
            throw new InvalidOperationException("The dialogue can only display strings or lists of strings!");
        }

        Instance.NextLine();
    }

    // Resets the dialogues and texts
    public void ZeroText() {
        Instance.WriteDialogueText("");
        index = 0;
    }

    // Goes to the next line for the dialogue or stops if reached the end
    public void NextLine() {
        if(index < dialogue.Count)
        {
            Instance.WriteDialogueText(dialogue[index]);
            index++;
        } else {
            StopDialogue();
        }
    }

    // Types a char from the dialog text a "charPerSecond"
    private IEnumerator Typing(string text) {
        foreach(char letter in text.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(charPerSecond);
        }
    }

    public void WriteDialogueText(string text) {
        dialoguePanel.SetActive(true);
        dialogueText.text = "";
        StartCoroutine(Typing(text));
    }

    public void SetDialogueName(string name) {
        dialogueName.text = name;
    }
}
