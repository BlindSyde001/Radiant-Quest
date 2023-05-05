using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIQuests : MonoBehaviour
{
    public static UIQuests Instance; // Singleton instance
    public GameObject questsPanel;
    [SerializeField] private TextMeshProUGUI questText;
    [SerializeField] private TextMeshProUGUI choresText;

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
        UpdateQuestList();
    }

    public void UpdateQuestText()
    {
        string text = "";

        Quest.ChoreList todayChores = Quest.Instance.TodayQuest();
        if (Quest.Instance.IsTodayQuestCompleted())
        {
            text += "X ";
        }
        text += todayChores.questName;

        questText.text = text;
    }

    public void UpdateChoresText()
    {
        string text = "";

        Quest.ChoreList choresList = Quest.Instance.TodayQuest();
        foreach (Chore chore in choresList.chores)
        {
            if (chore.isCompleted())
            {
                text += "X ";
            }
            text += chore.GetChoreDescription() + "\n";
        }

        choresText.text = text;
    }

    public void UpdateQuestList()
    {
        UpdateQuestText();
        UpdateChoresText();
    }
}
