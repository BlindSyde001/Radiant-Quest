using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BedController : MonoBehaviour
{
    public void PlayerAction() {
        GameManager.Instance.ChangeDay();
    }
}
