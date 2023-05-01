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
    public bool playerIsClose;

    private int index;

    void Start() {
        dialogueText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && playerIsClose) {
            if(!dialoguePanel.activeInHierarchy) {
                StartDialogue();
            } else if(dialogueText.text == dialogue[index]) {
                NextLine();
            }
        }

        if (Input.GetKeyDown(KeyCode.Q) && dialoguePanel.activeInHierarchy)
        {
            StopDialogue();
        }
    }

    public void StopDialogue() {
        ZeroText();
        dialoguePanel.SetActive(false);
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            playerIsClose = true;
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if(other.CompareTag("Player"))
        {
            playerIsClose = false;
            StopDialogue();
        }
    }
}
