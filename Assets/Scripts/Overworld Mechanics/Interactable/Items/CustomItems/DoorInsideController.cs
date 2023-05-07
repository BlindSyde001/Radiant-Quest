using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInsideController : InteractableController
{
    public int playerSpawningSpot;
    protected override void Interaction()
    {
        FindObjectOfType<GameManager>().playerSpawningSpot = playerSpawningSpot;
        ScenesManager.Instance.LoadScene($"Town_Level_Day{GameManager.GetDay() + 1}");
    }
}
