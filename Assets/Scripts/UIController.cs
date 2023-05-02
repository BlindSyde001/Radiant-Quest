using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public static UIController Instance { get; private set; } // Singleton instance
    public float charPerSecond;

    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private TextMeshProUGUI dialogueName;

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

    public bool isDialogueActive() {
        return dialoguePanel.activeInHierarchy;
    }

    public string GetDialogueText() {
        return dialogueText.text;
    }

    public void DialogueSetActive(bool isActive) {
        dialoguePanel.SetActive(isActive);
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
