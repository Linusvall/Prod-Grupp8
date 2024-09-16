using UnityEngine;
using System.Collections;

public class SequentialAudioPlayer : MonoBehaviour
{
    public AudioSource audioSource; // Reference to the AudioSource component
    public AudioClip firstClip;     // The first audio clip
    public AudioClip secondClip;    // The second audio clip

    void Start()
    {
        audioSource = GetComponent<AudioSource>(); 
        // Start playing the audio clips sequentially
        StartCoroutine(PlaySoundsSequentially());
    }

    IEnumerator PlaySoundsSequentially()
    {
        // Play the first audio clip
        audioSource.clip = firstClip;
        audioSource.Play();

        // Wait for the first clip to finish
        yield return new WaitForSeconds(firstClip.length);

        // Play the second audio clip
        audioSource.clip = secondClip;
        audioSource.Play();
    }
}
