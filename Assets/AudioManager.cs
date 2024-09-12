/*
 Grim
    Use this in other scripts that only call for one audio source:

        FindObjectOfType<AudioManager>().Play("Audio_Name", gameObject);


    If multiple sounds will be needed instead use:

        AudioManager aManager;

        aManager = FindObjectOfType<AudioManager>();

        aManager.Play("Audio_Name", gameObject);
 */

using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else 
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    public void Play (string name, GameObject gObject)
    {
        Sound s = Array.Find(sounds, sound  => sound.name == name);
        if (s == null)
        {
            Debug.LogError("Sound: " + name + " on " + gObject.name + " could not be found. Is it spelled correctly?");
            return;
            
        }

        // Check if the GameObject already has an AudioSource attached
        AudioSource audioSource = gObject.GetComponent<AudioSource>();
        if (audioSource == null)
        {
            // Create a new AudioSource on the GameObject
            audioSource = gObject.AddComponent<AudioSource>();
        }

        // Set the AudioSource properties from the Sound object
        audioSource.clip = s.clip;
        audioSource.volume = s.volume;
        audioSource.pitch = s.pitch;
        audioSource.loop = s.loop;
        audioSource.spatialBlend = 1;  // Assuming you want to keep 3D sound

        // Play the sound
        audioSource.Play();
    }
}
