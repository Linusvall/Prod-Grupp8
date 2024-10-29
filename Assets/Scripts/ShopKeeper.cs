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
    private int dialogueStep = 1;

    private bool completedTutorial = true; 

    private float SoundEffectEnumerator = 0;
    private bool HasExitedTheCollider = true; 

    private static readonly System.Random rand = new();

    private List<Func<IEnumerator>>  steps = new ();

    int step = 0; 

    // Start is called before the first frame update
    void Start()
    {
        if(CapCollider != null)
        {
            return; 
        }
        if(!TryGetComponent<CapsuleCollider>(out CapCollider))
        {
            Debug.Log("No collider on object");
            enabled = false;
            return;
        }

        steps.Add(PlayIntroDialogue);
        steps.Add(PlayDialogue);
        steps.Add(PlaySecondDialogue);


    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("StartFishing") && canInteract)
        {
            if (!completedTutorial)
            {
                StartCoroutine(steps[step].Invoke());
                return;
            }
            else
            {
               //Logger.Log("Shop started"); 
               StartShop();
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



    public void CompletedTutorial(bool state)
    {
        //Logger.Log("Shop activated");
        completedTutorial = state;
    }




    IEnumerator PlayIntroDialogue()
    {
        canInteract = false;
        audioSource.clip = introDialogue;
        audioSource.Play();
        yield return new WaitForSeconds(introDialogue.length);
        dialogueStep++;
        cavePortal.SetActive(true);
        canInteract = true;
        audioSource.clip = humming;
        audioSource.Play();

    }

    void StartShop()
    {
        if(ShopGUI != null)
        {
            ShopGUI.SetActive(true);
            canInteract = false;
            PlayerController.GetInstance().DisableMovement(false);
        }

    }


    IEnumerator PlayDialogue()
    {
        canInteract = false;
        audioSource.clip = repeatedDialogue;
        audioSource.Play();
        yield return new WaitForSeconds(repeatedDialogue.length);
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
        if (dialogueStep < steps.Count)
            dialogueStep++;
    }



}
