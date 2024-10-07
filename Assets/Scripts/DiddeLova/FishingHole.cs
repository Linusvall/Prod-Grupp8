using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingHole : MonoBehaviour
{

    private bool canInteract = false;
    [SerializeField] private AudioClip[] fishingClips;
    [SerializeField] private AudioClip[] scaryClips;
    [SerializeField] private GameObject fishingHoleManager;
    private AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        source.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("StartFishing") && canInteract)
        {
            StartCoroutine(StartFishing());
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Interact"))
        {
            canInteract = true;
            Debug.Log("Can interact with fishing hole");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Interact"))
        {
            canInteract = false;
            Debug.Log("Can not interact with fishing hole");
        }
    }

    private IEnumerator StartFishing()
    {
        canInteract = false;
        Debug.Log("Fishing");
        source.Stop();
        source.PlayOneShot(fishingClips[Random.Range(0, fishingClips.Length)]);
        source.PlayOneShot(scaryClips[Random.Range(0, scaryClips.Length)]);
        yield return new WaitForSeconds(4);
        fishingHoleManager.GetComponent<FishingHoleManager>().OpenNewFishingHole();
        gameObject.SetActive(false);
        Debug.Log("Complete!");
    }

}
