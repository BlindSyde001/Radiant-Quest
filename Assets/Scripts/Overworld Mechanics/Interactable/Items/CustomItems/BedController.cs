using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BedController : InteractableController
{
    protected override void Interaction()
    {
        GameManager.Instance.ChangeDay();
    }
}
