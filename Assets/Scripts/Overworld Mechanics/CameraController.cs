using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;

    private void Start()
    {
        player = FindObjectOfType<PlayerController>().gameObject;
    }
    private void Update()
    {
        transform.position = new Vector3(player.transform.position.x,player.transform.position.y, -10);
    }
}