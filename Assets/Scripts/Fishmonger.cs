using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fishmonger : MonoBehaviour
{

    private AudioSource audioSource;
    private bool interacted = false;
    [SerializeField] private AudioClip humming;
    [SerializeField] private AudioClip interact;


    // Start is called before the first frame update
    void Start()
    {
        audioSource= GetComponent<AudioSource>();
        audioSource.clip = humming;
        audioSource.loop=true;
        audioSource.Play();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!interacted)
        {
            audioSource.Stop();
            audioSource.loop = false;
            audioSource.volume= 0.4f;
            audioSource.clip = interact;
            audioSource.Play();
            interacted = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        audioSource.clip = humming;
        audioSource.volume = 0.6f;
        audioSource.loop = true;
        audioSource.Play();
    }
}
