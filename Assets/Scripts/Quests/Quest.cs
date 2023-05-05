using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Quest : MonoBehaviour
{
    public static Quest Instance; // Singleton instance
    public bool isCompleted;

    [System.Serializable]
    public class ChoreList
    {
        public string questName;
        public List<Chore> chores;

        public ChoreList()
        {
            chores = new List<Chore> { };
        }
    }
    [SerializeField]
    public List<ChoreList> choresList = new List<ChoreList>();

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
        isCompleted = false;
    }

    void CompleteQuest()
    {
        isCompleted = true;
    }

    // Get all the chores for today
    public ChoreList TodayQuest()
    {
        int today = GameManager.GetDay();
        if (today > choresList.Count - 1) return new ChoreList();
        return choresList[today];
    }

    public bool IsTodayQuestCompleted()
    {
        ChoreList todayQuests;
        try
        {
            todayQuests = TodayQuest();
        }
        catch (InvalidOperationException)
        {
            return true;
        }
        foreach (Chore chore in todayQuests.chores)
        {
            if (!chore.isCompleted()) return false;
        }
        return true;
    }

    public Chore CurrentChore()
    {
        ChoreList todayQuests = TodayQuest();

        foreach (Chore chore in todayQuests.chores)
        {
            if (chore.isCompleted()) continue;
            return chore;
        }
        throw new InvalidOperationException("There are no more quests today!");
    }

    public void EvaluateAction(GameObject actionObject)
    {
        Chore current = CurrentChore();
        current.Evaluate(actionObject);
        UIQuests.Instance.UpdateQuestList();
    }
}
