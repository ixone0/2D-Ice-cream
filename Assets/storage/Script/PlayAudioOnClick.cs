using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayAudioOnClick : MonoBehaviour
{
    public AudioSource audioSource; // Reference to the AudioSource component

    void Start()
    {
        // If audioSource is not assigned, get the AudioSource component on this GameObject
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
    }

    // Method to play audio when the button is clicked
    public void PlayAudio()
    {
        if (audioSource != null)
        {
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("AudioSource is not assigned or missing!");
        }
    }
}