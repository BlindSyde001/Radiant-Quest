using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; } // Singleton instance
    [SerializeField] private int day; // Private variable to store the current day
    [SerializeField] private List<int> numChores = new List<int>{}; // Number of chores/day that the player must accomplish

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

    void Start() {
    }

    // Getter for the day variable
    public int GetDay()
    {
        return day;
    }

    // Setter for the day variable
    public void ChangeDay()
    {
        if(TodayChores() > 0) return;
        day += 1;
    }

    // Decreases the chore number for the current day
    public void ChoreAccomplished()
    {
        if(numChores.Count == 0 || numChores[day] == 0) return;

        numChores[day] -= 1;
    }

    // Get the number of chores for today
    public int TodayChores() {
        return numChores[day];
    }
}

