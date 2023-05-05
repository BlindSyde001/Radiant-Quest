using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class NPCController : InteractableController
{
    public List<string> dialogue = new List<string> { };
    public string defaultErrorMessage = "I can't talk right now!";

    protected override void Interaction()
    {
        if (!_interactionActive)
        {
            throw new InvalidActionException(defaultErrorMessage, objName);
        }

        UIDialogue.Instance.StartDialogue(objName, dialogue);
    }
}
