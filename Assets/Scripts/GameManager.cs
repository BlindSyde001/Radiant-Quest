using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // Singleton instance
    public int day; // Private variable to store the current day

    public Quest quests; // All the quests lists

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

    void Start()
    {
        quests = GameObject.Find("Quests").GetComponent<Quest>();
    }

    public static int GetDay()
    {
        return Instance.day;
    }

    // Setter for the day variable
    public void ChangeDay()
    {
        if (!quests.IsTodayQuestCompleted())
        {
            throw new InvalidActionException("Cannot change day while there are pending chores!");
        }
        day += 1;
        UIController.Instance.StartDialogue("", $"Day {day} started!");
    }
}

