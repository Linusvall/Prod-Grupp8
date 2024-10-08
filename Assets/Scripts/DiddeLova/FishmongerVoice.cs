using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishmongerVoice : MonoBehaviour
{
    [SerializeField] private AudioClip introDialogue;
    [SerializeField] private AudioClip repeatedDialogue;
    [SerializeField] private AudioClip secondDialogue;
    [SerializeField] private AudioClip humming;
    [SerializeField] private GameObject cavePortal;
    [SerializeField] private GameObject swampPortal;
    private AudioSource audioSource;
    private bool canInteract = false;
    private int dialogueStep = 1;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetButtonDown("StartFishing") && canInteract && dialogueStep == 1)
        {
            StartCoroutine(PlayIntroDialogue());
        }
        if (Input.GetButtonDown("StartFishing") && canInteract && dialogueStep == 2)
        {
            StartCoroutine(PlayDialogue(repeatedDialogue));
        }
        if (Input.GetButtonDown("StartFishing") && canInteract && dialogueStep == 3)
        {
            StartCoroutine(PlaySecondDialogue());
        }

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Interact") && audioSource.clip != introDialogue)
        {
            canInteract = true;
            Debug.Log("Can interact with portal");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Interact"))
        {
            canInteract = false;
            Debug.Log("Can not interact with portal");
        }
    }

    IEnumerator PlayIntroDialogue()
    {
        canInteract= false;
        audioSource.clip= introDialogue;
        audioSource.Play();
        yield return new WaitForSeconds(introDialogue.length);
        dialogueStep++;
        cavePortal.SetActive(true);
        canInteract= true;
        audioSource.clip = humming;
        audioSource.Play();

    }

    IEnumerator PlayDialogue(AudioClip dialogue)
    {
        canInteract = false;
        audioSource.clip = dialogue;
        audioSource.Play();
        yield return new WaitForSeconds(dialogue.length);
        canInteract = true;
        audioSource.clip = humming;
        audioSource.Play();

    }

    IEnumerator PlaySecondDialogue()
    {
        canInteract = false;
        audioSource.clip = secondDialogue;
        audioSource.Play();
        yield return new WaitForSeconds(secondDialogue.length);
        dialogueStep++;
        swampPortal.SetActive(true);
        canInteract = true;
        audioSource.clip = humming;
        audioSource.Play();

    }

    public void ProceedDialogue()
    {
        dialogueStep++;
    }

}
