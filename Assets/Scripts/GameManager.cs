using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.Loading;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // Singleton instance
    public int day; // Private variable to store the current day
    public int playerSpawningSpot;

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

    private void OnEnable()
    {
        day = 3;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
       if(FindAnyObjectByType<PlayerController>() != null && scene.name.Contains("Town") )
        {
            FindAnyObjectByType<PlayerController>().transform.position = FindAnyObjectByType<LoadingSpotContainer>().spot[playerSpawningSpot].position;
        }
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
            throw new InvalidActionException("I need to complete my daily chores before I go to bed.");

            
        }
        day += 1;
        UIQuests.Instance.UpdateQuestList();
        UIDialogue.Instance.StartDialogue("", $"Day {day + 1} started!");
    }
}

