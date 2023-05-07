using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOutsideController : InteractableController
{
    public string scene;

    protected override void Interaction()
    {
        ScenesManager.Instance.LoadScene(scene);
    }
}
