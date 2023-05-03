using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class CollectibleItemController : InteractableController
{
    protected override void Interaction()
    {
        if (!_interactionActive)
        {
            throw new InvalidActionException($"You can't pick {objName} yet!");
        }

        UIController.Instance.StartDialogue("", $"You picked the {objName}!");
    }
}
