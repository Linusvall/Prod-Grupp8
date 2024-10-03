using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class VoidPortal : MonoBehaviour
{

    private AudioSource source;
    private bool canInteract = false;
    [SerializeField] private GameObject player;
    [SerializeField] private Transform destination;
   
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
            Debug.Log("Teleported");
            TransportPlayer();
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

    private void TransportPlayer()
    {
        Debug.Log("Teleported again");

        characterController.enabled = false;
        player.transform.position = destination.position;   
        characterController.enabled = true;

        Debug.Log("Teleported again");
    }
}
