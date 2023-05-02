using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;
    public string[] dialogue;
    public float charPerSecond;
    public bool isTalking = false;

    private int index;

    void Start() {
        dialogueText.text = "";
    }

    public void PlayerAction() {
        if(dialoguePanel == null) return;

        if(!dialoguePanel.activeInHierarchy) {
            isTalking = true;
            StartDialogue();
        } else if(dialogueText.text == dialogue[index]) {
            NextLine();
        }
    }

    public void StopDialogue() {
        ZeroText();
        dialoguePanel.SetActive(false);
        isTalking = false;
    }

    public void StartDialogue() {
        ZeroText();
        dialoguePanel.SetActive(true);
        StartCoroutine(Typing());
    }

    // Resets the dialogues and texts
    public void ZeroText() {
        dialogueText.text = "";
        index = 0;
    }

    // Types a char from the dialog text a "charPerSecond"
    IEnumerator Typing() {
        foreach(char letter in dialogue[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(charPerSecond);
        }
    }

    // Goes to the next line for the NPC or stops if reached the end of the dialogue
    public void NextLine() {
        if(index < dialogue.Length - 1)
        {
            index++;
            dialogueText.text = "";
            StartCoroutine(Typing());
        } else {
            StopDialogue();
        }
    }
}
