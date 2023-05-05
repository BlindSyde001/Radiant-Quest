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
                if (logString == "InvalidActionException") return;
                string messageWithName = logString.Substring(logString.IndexOf(":") + 1).Trim();
                string objName = messageWithName.Substring(0, messageWithName.IndexOf(":"));
                string message = messageWithName.Substring(messageWithName.IndexOf(":") + 1).Trim();
                UIDialogue.Instance.StartDialogue(objName, message);
            }
        }
    }
}
