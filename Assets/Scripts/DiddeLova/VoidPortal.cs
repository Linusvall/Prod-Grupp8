using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class VoidPortal : MonoBehaviour
{

    private AudioSource source;
    private bool canInteract = false;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject fishingHoleManager;
    [SerializeField] private Transform destination;
    [SerializeField] private AudioClip teleportSound;
    [SerializeField] private AudioClip portalOpenClip;
    [SerializeField] private GameObject ambience;
    [SerializeField] private GameObject audioCompass;
    [SerializeField] private GameObject audioCompassNewLocation;
    [SerializeField] private AudioClip audioCompassNewClip;
    [SerializeField] private string footstepType;
   
    CharacterController characterController;


    void Start()
    {
        characterController = player.GetComponent<PlayerController>().controller;
    }

    private void OnEnable()
    {
        source = GetComponent<AudioSource>();
        source.Play();
        source.PlayOneShot(portalOpenClip);
    }

    void Update()
    {
        if (Input.GetButtonDown("StartFishing") && canInteract)
        {
            StartCoroutine(TransportPlayer());
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Interact"))
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

    private IEnumerator TransportPlayer()
    {
        characterController.enabled = false;
        player.transform.position = destination.position;

        Debug.Log("Teleported");

        source.PlayOneShot(teleportSound);
        yield return new WaitForSeconds(3);
        characterController.enabled = true;
        player.GetComponent<PlayerController>().ChangeFootstep(footstepType);

        if(fishingHoleManager.activeSelf == false)
        {
            audioCompass.GetComponent<ObjectiveCompass>().ChangeAudioCompassPosition(audioCompassNewLocation, audioCompassNewClip);
            fishingHoleManager.SetActive(true);
            ambience.SetActive(true);
        }
        else 
        {
            audioCompass.GetComponent<ObjectiveCompass>().ChangeAudioCompassPosition(audioCompassNewLocation, audioCompassNewClip);
            fishingHoleManager.SetActive(false);
            ambience.SetActive(false);
        }

        Debug.Log("Can move again");
    }
}
