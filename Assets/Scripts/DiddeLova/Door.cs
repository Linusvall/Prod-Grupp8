using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] AudioClip openDoorClip;
    [SerializeField] AudioClip closeDoorClip;
    [SerializeField] GameObject closeDoorBarrier;
    [SerializeField] GameObject fishmonger;
    [SerializeField] GameObject interactRadius;
    private AudioSource fishmongerAudio;
    private AudioSource audioSource;
    bool canInteract;
    bool hasBeenOpened;
    bool canEnter = true;

   

    // Start is called before the first frame update
    void Start()
    {
      audioSource = GetComponent<AudioSource>();
        fishmongerAudio = fishmonger.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("StartFishing") && canInteract) {

            OpenDoor();
        }
  

    }

    private void OnTriggerExit(UnityEngine.Collider other)
    {
        if (other.gameObject.CompareTag("Player") ) {
       
            CloseDoor();
    }
        return;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Interact"))
        {
            canInteract = true;
            
        }
    }
    private void OpenDoor()
    {
        if (canEnter)
        {
            closeDoorBarrier.SetActive(false);
            hasBeenOpened = true;
            audioSource.PlayOneShot(openDoorClip);
            canEnter = false;
        }
        
    }
    private void CloseDoor()
    {
        if (hasBeenOpened)
        {
            audioSource.PlayOneShot(closeDoorClip);
            Debug.Log("Door is closed");
            hasBeenOpened=false;
            closeDoorBarrier.SetActive(true);
            fishmongerAudio.Play();

        }
       
        
    }

}
