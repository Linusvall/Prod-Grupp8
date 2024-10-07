using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractSound : MonoBehaviour
{
    private AudioSource source;
    private bool canInteract;
    private float fadeDuration;
    private float startVolume;

    
    void Start()
    {
        source = GetComponent<AudioSource>();
        canInteract = false;
        fadeDuration = 0.4f;
        startVolume = source.volume;
    }

    void Update()
    {
        if (canInteract && Input.GetButtonDown("StartFishing")) {
            PauseInteractSound();
        }


    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 6)
        {
            canInteract = true;
            StartInteractSound();
            source.volume = startVolume;
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            FadeOutInteractSound();
            canInteract = false;
        }
    }






    public void FadeOutInteractSound()
    {
        StartCoroutine(FadeOutVolume());
    }

    public void PauseInteractSound()
    {
        source.Pause();
    }

    public void StartInteractSound()
    {
    source.volume = startVolume;
    source.Play(); 
    }


    IEnumerator FadeOutVolume()
    {
        float startVolume = source.volume;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            source.volume = Mathf.Lerp(startVolume, 0f, elapsedTime / fadeDuration);
            yield return null; 
        }

        
       source.volume = 0f;
    }
}
