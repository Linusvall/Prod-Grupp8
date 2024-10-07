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
   
    CharacterController characterController;


    void Start()
    {
        characterController = player.GetComponent<PlayerController>().controller;
    }

    private void OnEnable()
    {
        source = GetComponent<AudioSource>();
        source.Play();
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
        yield return new WaitForSeconds(teleportSound.length);
        characterController.enabled = true;

        if(fishingHoleManager.activeSelf == false)
            fishingHoleManager.SetActive(true);
        else fishingHoleManager.SetActive(false);

        Debug.Log("Can move again");
    }
}
