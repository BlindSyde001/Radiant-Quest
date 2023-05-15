using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class NPCController : InteractableController
{
    public List<string> dialogue = new List<string> { };
    public string secondDialogueLine = "I can't talk right now!";
    //public string defaultErrorMessage = "I can't talk right now!";

    protected override void Interaction()
    {
        if (!_interactionActive) {

            UIDialogue.Instance.StartDialogue(objName, secondDialogueLine);
            //throw new InvalidActionException(defaultErrorMessage, objName);
        } else {

            UIDialogue.Instance.StartDialogue(objName, dialogue);
            ActivateInteraction(false);

        }
    }

    void RemoveSelf() {
        this.enabled = false;
    }
}
