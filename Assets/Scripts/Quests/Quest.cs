using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Quest : MonoBehaviour
{
    public string questName;
    public bool isCompleted;

    [System.Serializable]
    public class ChoreList
    {
        public List<Chore> chores;
    }
    [SerializeField]
    public List<ChoreList> choresList = new List<ChoreList>();

    void Awake()
    {
        DontDestroyOnLoad(gameObject); // Persist across scene loads
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
    public List<Chore> TodayQuest()
    {
        int today = GameManager.GetDay();
        if (today > choresList.Count - 1) return new List<Chore> { };
        return choresList[today].chores;
    }

    public bool IsTodayQuestCompleted()
    {
        List<Chore> todayQuests;
        try
        {
            todayQuests = TodayQuest();
        }
        catch (InvalidOperationException)
        {
            return true;
        }
        foreach (Chore chore in todayQuests)
        {
            if (!chore.isCompleted()) return false;
        }
        return true;
    }

    public Chore CurrentChore()
    {
        List<Chore> todayQuests = TodayQuest();

        foreach (Chore chore in todayQuests)
        {
            if (chore.isCompleted()) continue;
            return chore;
        }
        throw new InvalidOperationException("There are no more quests today!");
    }

    public void EvaluateAction(GameObject actionObject)
    {
        Chore current;
        try
        {
            current = CurrentChore();
        }
        catch (InvalidOperationException)
        {
            return;
        }
        current.Evaluate(actionObject);
    }
}
