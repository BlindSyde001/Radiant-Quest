using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseDoorOutsideController : InteractableController
{
    protected override void Interaction()
    {
        ScenesManager.Instance.LoadScene("Home");
    }
}
