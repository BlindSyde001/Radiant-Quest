using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle : MonoBehaviour
{
    public bool canDriveOnGround;
    public bool canDriveOnSea;
    public bool canDriveOnMountain;
    public bool canDriveOnShipExit;

    public float vehicleSpeed = 10;
}