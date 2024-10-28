using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] AudioClip openDoorClip;
    [SerializeField] AudioClip closeDoorClip;
    [SerializeField] AudioClip[] doorCreaks;
    [SerializeField] GameObject closeDoorBarrier;
    [SerializeField] GameObject player;
    [SerializeField] GameObject fishmonger;
    [SerializeField] GameObject interactRadius;
    [SerializeField] GameObject outsideAmbience;
    private AudioSource fishmongerAudio;
    private AudioSource audioSource;
    private bool canInteract;
    private bool hasBeenOpened;
    private bool canEnter = true;
    private int clipIndex;

   

    // Start is called before the first frame update
    void Start()
    {
      audioSource = GetComponent<AudioSource>();
        fishmongerAudio = fishmonger.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(hasBeenOpened == false && audioSource.isPlaying == false)
        {
            clipIndex = Random.Range(1, doorCreaks.Length);
            AudioClip clip = doorCreaks[clipIndex];
            audioSource.PlayOneShot(clip);
            doorCreaks[clipIndex] = doorCreaks[0];
            doorCreaks[0] = clip;
        }


        if (Input.GetButtonDown("StartFishing") && canInteract) {

            OpenDoor();
        }
  

    }

    private void OnTriggerExit(UnityEngine.Collider other)
    {
        if (other.gameObject.CompareTag("Player") ) {
       
            StartCoroutine(CloseDoor());
    }
        return;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Interact"))
        {
            canInteract = true;
            //player.GetComponent<PlayerController>().ChangeFootstep("Floor");
        }
    }
    private void OpenDoor()
    {
        if (canEnter)
        {
            audioSource.Stop();
            audioSource.volume= 1.0f;
            closeDoorBarrier.SetActive(false);
            hasBeenOpened = true;
            audioSource.PlayOneShot(openDoorClip);
            canEnter = false;
        }
        
    }
    private IEnumerator CloseDoor()
    {
        if (hasBeenOpened)
        {
            audioSource.PlayOneShot(closeDoorClip);
            Debug.Log("Door is closed");
            closeDoorBarrier.SetActive(true);
            yield return new WaitForSeconds(0.2f);
            outsideAmbience.GetComponent<AudioLowPassFilter>().enabled = true;
            yield return new WaitForSeconds(closeDoorClip.length);
            fishmongerAudio.Play();
            gameObject.SetActive(false);
        }
       
        
    }

}
