using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
public class ShopKeeper : MonoBehaviour
{


    public CapsuleCollider CapCollider;
    public string SoundEffectToPlayOnEnter;
    public string SoundEffectToPlayOnExit;
    public string SoundEffectToPlayRandomly;
    public int ChanceToPlaySoundEffect;
    public float FrequencyToPlayEffect;

    [SerializeField] private AudioClip introDialogue;
    [SerializeField] private AudioClip repeatedDialogue;
    [SerializeField] private AudioClip secondDialogue;
    [SerializeField] private AudioClip humming;
    [SerializeField] private GameObject cavePortal;
    [SerializeField] private GameObject swampPortal;
    [SerializeField] GameObject ShopGUI;

    [SerializeField] private AudioSource audioSource;
    private bool canInteract = false;
    [SerializeField] private int dialogueStep = 1;

    private float SoundEffectEnumerator = 0;
    private bool HasExitedTheCollider = true; 

    private static readonly System.Random rand = new();

    // Start is called before the first frame update
    void Start()
    {
        if(!TryGetComponent<CapsuleCollider>(out CapCollider))
        {
            Debug.Log("No collider on object");
            enabled = false;
            return;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("StartFishing") && canInteract)
        {
            StartShop();
            if (true)
            {
                return; 
            }
            switch (dialogueStep)
            {
                case 1:
                    StartCoroutine(PlayIntroDialogue());
                    break;
                case 2:
                    StartCoroutine(PlayRepeatedDialogue());
                    break;
                case 3:
                    StartCoroutine(PlaySecondDialogue());
                    break;
                default:
                    StartShop();
                    break;
            }

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
        canInteract = false;
        audioSource.clip = introDialogue;
        audioSource.Play();
        yield return new WaitForSeconds(introDialogue.length);
        dialogueStep++;
        cavePortal.SetActive(true);
        audioSource.clip = humming;
        audioSource.Play();

    }

    void StartShop()
    {
        if(ShopGUI != null)
        {
            audioSource.Stop();
            ShopGUI.SetActive(true);
            canInteract = false;
            PlayerController.GetInstance().DisableMovement(false);
        }

    }


    IEnumerator PlayRepeatedDialogue()
    {
        canInteract = false;
        audioSource.clip = repeatedDialogue;
        audioSource.Play();
        yield return new WaitForSeconds(repeatedDialogue.length);
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
        audioSource.clip = humming;
        audioSource.Play();
        
    }

        public void ProceedDialogue()
    {
        if(dialogueStep < 3)
        dialogueStep++;
    }



}
