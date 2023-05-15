using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.Loading;
using UnityEngine.Video;

public class WellBenEnder : InteractableController {

    public VideoPlayer videoPlayer;
    public GameObject videoPlayerObj;

    //public VideoManager videoManager;
    private bool endingPlayed = false;

    protected override void Interaction() {

        if (!endingPlayed) {

            if (!Quest.Instance.IsTodayQuestCompleted()) {
                //throw new InvalidActionException("I still need to find the cotton and the weapon before I go down the well!");
                UIDialogue.Instance.StartDialogue("", "I still need to find the cotton and the weapon before I go down the well!");
                return;
            }

            endingPlayed = true;
            PlayFinalBattle();
            StartCoroutine(RemoveVideo());

        }

    }

    IEnumerator RemoveVideo() {
        yield return new WaitForSeconds(20f);
        RemoveVideoAgain();

    }

    public void PlayFinalBattle() {
        Debug.Log(videoPlayerObj);
        videoPlayerObj.SetActive(true);
        videoPlayer.Play();
    }

    public void RemoveVideoAgain() {
        videoPlayerObj.SetActive(false);
        ScenesManager.Instance.endGame();
    }


}



