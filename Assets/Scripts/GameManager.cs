using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // Singleton instance
    public int day; // Private variable to store the current day

    // State of the game variables
    public enum GameStates
    {
        MainMenu,       // The game is in the main menu
        Playing,        // The game is actively being played
        Paused,         // The game is paused
        GameOver        // The game is over
    }
    public static GameStates state;

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
        state = GameStates.MainMenu;
    }

    public static int GetDay()
    {
        return Instance.day;
    }

    // Setter for the day variable
    public void ChangeDay()
    {
        if (!Quest.Instance.IsTodayQuestCompleted())
        {
            throw new InvalidActionException("Cannot change day while there are pending chores!");
        }
        day += 1;
        ScenesManager.Instance.LoadScene($"Town_Level_Day{day + 1}");
        UIQuests.Instance.UpdateQuestList();
        UIDialogue.Instance.StartDialogue("", $"Day {day + 1} started!");
    }
}

