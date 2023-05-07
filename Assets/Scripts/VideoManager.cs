using System.Collections;
using System.Collections.Generic;
using UnityEngine.Video;
using UnityEngine;

public class VideoManager : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public GameObject videoPlayerObj;

    public void PlayFinalBattle() {
        Debug.Log("Playing");
        videoPlayerObj.SetActive(true);
        videoPlayer.Play();
    }
}
