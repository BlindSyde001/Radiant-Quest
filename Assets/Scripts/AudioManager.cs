using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource backgroundSound;
    public AudioSource breadSound;
    public static AudioManager Instance; // Singleton instance

    private void Awake()
    {
        // Create or destroy duplicate instances
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scene loads
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlaySound(AudioSource audioSource)
    {
        audioSource.Play();
    }
}
