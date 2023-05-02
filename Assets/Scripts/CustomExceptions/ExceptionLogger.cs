using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExceptionLogger : MonoBehaviour
{
    public static ExceptionLogger instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        Application.logMessageReceived += HandleException;
    }

    private void OnDisable()
    {
        Application.logMessageReceived -= HandleException;
    }

    private void HandleException(string logString, string stackTrace, LogType type)
    {
        if (type == LogType.Exception)
        {
            if (logString.StartsWith("InvalidActionException"))
            {
                string message = logString.Substring(logString.IndexOf(":") + 1).Trim();
                UIController.Instance.StartDialogue("", message);
            }
        }
    }
}
