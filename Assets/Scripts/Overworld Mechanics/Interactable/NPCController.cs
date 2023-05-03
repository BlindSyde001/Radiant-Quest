using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class NPCController : InteractableController
{
    public List<string> dialogue = new List<string>{};

    protected override void Interaction() {
            if(!_interactionActive) {
            throw new InvalidActionException($"You can't talk to {objName} yet!");
        }

        if(!UIController.Instance.isDialogueActive()) {
            UIController.Instance.StartDialogue(objName, dialogue);
        }
    }
}
