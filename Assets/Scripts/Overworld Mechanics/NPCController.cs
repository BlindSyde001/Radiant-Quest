using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    public string npcName;
    public string[] dialogue;

    private int index;

    void Start() {
        
    }

    public void PlayerAction() {
        if(!UIController.Instance.isDialogueActive()) {
            StartDialogue();
        } else if(UIController.Instance.GetDialogueText() == dialogue[index]) {
            NextLine();
        }
    }

    public void StopDialogue() {
        ZeroText();
        UIController.Instance.DialogueSetActive(false);
    }

    public void StartDialogue() {
        ZeroText();
        UIController.Instance.SetDialogueName(npcName);
        UIController.Instance.WriteDialogueText(dialogue[index]);
    }

    // Resets the dialogues and texts
    public void ZeroText() {
        UIController.Instance.WriteDialogueText("");
        index = 0;
    }

    // Goes to the next line for the NPC or stops if reached the end of the dialogue
    public void NextLine() {
        if(index < dialogue.Length - 1)
        {
            index++;
            UIController.Instance.WriteDialogueText(dialogue[index]);
        } else {
            StopDialogue();
        }
    }
}
