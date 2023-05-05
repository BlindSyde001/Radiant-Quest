using System.Collections;
using System.Collections.Generic;
using System;
using TMPro;
using UnityEngine;

public class UIDialogue : MonoBehaviour
{
    public static UIDialogue Instance; // Singleton instance

    private const float CHAR_PER_SECOND = 0.05f;
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private TextMeshProUGUI dialogueName;

    private static int index;
    private static bool isTyping = false;
    private static List<string> dialogue = new List<string> { };

    private void Awake()
    {
        // Create or destroy duplicate instances
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scene loads
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
    public bool isDialogueActive()
    {
        return dialoguePanel.activeInHierarchy;
    }

    public string GetDialogueText()
    {
        return dialogueText.text;
    }

    public void DialogueSetActive(bool isActive)
    {
        dialoguePanel.SetActive(isActive);
    }

    public void StopDialogue()
    {
        ZeroText();
        Instance.DialogueSetActive(false);
        GameManager.state = GameManager.GameStates.Playing;
    }

    // Starts a new dialogue on the scene and configures the name and list of lines for the dialogue
    public void StartDialogue(string dialogueName, object dialogue_)
    {
        GameManager.state = GameManager.GameStates.Paused;
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
            dialogue = new List<string> { dialogueString };
        }
        else
        {
            throw new InvalidOperationException("The dialogue can only display strings or lists of strings!");
        }

        Instance.NextLine();
    }

    // Resets the dialogues and texts
    public void ZeroText()
    {
        Instance.WriteDialogueText("");
        index = 0;
    }

    // Goes to the next line for the dialogue or stops if reached the end
    public void NextLine()
    {
        if (isTyping)
        {
            isTyping = false;
            dialogueText.text = dialogue[index];
        }
        else if (index < dialogue.Count)
        {
            Instance.WriteDialogueText(dialogue[index]);
        }
        else
        {
            StopDialogue();
        }
    }

    // Types a char from the dialog text a "CHAR_PER_SECOND"
    private IEnumerator Typing(string text)
    {
        isTyping = true;
        foreach (char letter in text.ToCharArray())
        {
            if (!isTyping) break;
            dialogueText.text += letter;
            yield return new WaitForSeconds(CHAR_PER_SECOND);
        }
        isTyping = false;
        index++;
    }

    public void WriteDialogueText(string text)
    {
        dialoguePanel.SetActive(true);
        dialogueText.text = "";
        StartCoroutine(Typing(text));
    }

    public void SetDialogueName(string name)
    {
        dialogueName.text = name;
    }
}
