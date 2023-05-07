using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInsideController : InteractableController
{
    protected override void Interaction()
    {
        ScenesManager.Instance.LoadScene($"Town_Level_Day{GameManager.GetDay() + 1}");
    }
}
