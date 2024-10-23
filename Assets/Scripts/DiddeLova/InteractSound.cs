using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractSound : MonoBehaviour
{
    [SerializeField] private AudioClip getBackClip;
    [SerializeField] private GameObject fishmonger;
    [SerializeField] private GameObject fishHoleManager1;
    [SerializeField] private GameObject fishHoleManager2;
    private AudioSource source;
    private bool canInteract;
    private bool canGetFish; //temp
    private float timeToFade;
    private float startVolume;
    private int fishCounter;

    
    void Start()
    {
        source = GetComponent<AudioSource>();
        canInteract = false;
        timeToFade = 0.4f;
        startVolume = 0;
    }

    void Update()
    {
        if (canInteract && Input.GetButtonDown("StartFishing")) {
            PauseInteractSound();
            canInteract= false;
            if(canGetFish)
            {
                fishCounter++;
                if(fishCounter >= 3) 
                {
                    source.Stop();
                    source.PlayOneShot(getBackClip);
                    fishmonger.GetComponent<FishmongerVoice>().ProceedDialogue();
                    fishCounter = 0;
                    fishHoleManager1.SetActive(false);
                    fishHoleManager2.SetActive(false);
                }
            }
        }


    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 6)
        {
            StartCoroutine(FadeInVolume());
            canInteract = true;

            if(other.gameObject.CompareTag("FishingHole"))
            {
                canGetFish = true;
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            StartCoroutine(FadeOutVolume());
            canInteract = false;
            canGetFish = false;
        }
    }

    public void PauseInteractSound()
    {
        source.Pause();
    }

    IEnumerator FadeInVolume()
    {
        source.volume = 0f;
        source.Play();

        float timer = 0f;

        while (timer < timeToFade)
        {
            timer += Time.deltaTime;
            source.volume = Mathf.Lerp(startVolume, 1f, timer / timeToFade);
            yield return null;
        }
    }


    IEnumerator FadeOutVolume()
    {
        float timer = 0f;

        while (timer < timeToFade)
        {
           timer += Time.deltaTime;
            source.volume = Mathf.Lerp(source.volume, 0f, timer / timeToFade);
            yield return null; 
        }

        
       source.volume = 0f;
    }
}
