using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ScenesManager : MonoBehaviour
{
    public AudioClip startGameAudio;
    private bool loadingScene = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown && GameManager.state == GameManager.GameStates.MainMenu)
        {
            if (loadingScene)
                return;

            loadingScene = true;
            if (startGameAudio != null)
            {
                AudioSource.PlayClipAtPoint(startGameAudio, Vector3.zero);
            }
            LoadScene("Town_Level");
            UIQuests.Instance.questsPanel.SetActive(true);
            GameManager.state = GameManager.GameStates.Playing;
        }
    }

    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
        loadingScene = false;
    }
}
