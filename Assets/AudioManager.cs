/*
 Grim
    Use this in other scripts that only call for one audio source:

        FindObjectOfType<AudioManager>().Play("Audio_Name");


    If multiple sounds will be needed instead use:

        AudioManager aManager;

        aManager = FindObjectOfType<AudioManager>();

        aManager.Play("Audio_Name");
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

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    public void Play (string name)
    {
        Sound s = Array.Find(sounds, sound  => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " could not be found. Is it spelled correctly?");
            return;
        }
        s.source.Play();
    }
}
