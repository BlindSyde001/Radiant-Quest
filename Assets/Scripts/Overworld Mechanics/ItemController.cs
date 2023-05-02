using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : InteractableController
{
    // Custom action to call on player interaction with this object
    [SerializeField] private UnityEngine.Events.UnityEvent interactAction;

    public override void PlayerInteract()
    {
        interactAction.Invoke();
    }
}
