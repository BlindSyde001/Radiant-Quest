using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Chore : MonoBehaviour
{
    [SerializeField] protected string goalObjName;

    [SerializeField] private string choreDescription;
    [SerializeField] private bool _isCompleted;

    // Custom action to call after quest is completed
    [SerializeField] private UnityEngine.Events.UnityEvent unlockAction;

    protected virtual void Start()
    {
        _isCompleted = false;
    }

    public bool isCompleted()
    {
        return _isCompleted;
    }

    public virtual void Evaluate(GameObject actionObject)
    {
        throw new InvalidOperationException("Must be implemented!");
    }

    protected void CompleteChore()
    {
        _isCompleted = true;
        unlockAction.Invoke();
    }
}
