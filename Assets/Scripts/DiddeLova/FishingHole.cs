using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingHole : MonoBehaviour
{

    private bool canInteract = false;
    [SerializeField] private AudioClip[] fishingClips;
    [SerializeField] private AudioClip[] scaryClips;
    [SerializeField] private GameObject fishingHoleManager;
    [SerializeField] private float fadeDuration;
    private AudioSource source;

    // Start is called before the first frame update
    void OnEnable()
    {
        source = GetComponent<AudioSource>();
        source.volume = 0;
        source.Play();
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        float startVolume = source.volume;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            source.volume = Mathf.Lerp(startVolume, 1f, elapsedTime / fadeDuration);
            yield return null;
        }
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
        source.volume = 0.5f;
        source.PlayOneShot(fishingClips[Random.Range(0, fishingClips.Length)]);
        source.PlayOneShot(scaryClips[Random.Range(0, scaryClips.Length)]);
        yield return new WaitForSeconds(3);
        fishingHoleManager.GetComponent<FishingHoleManager>().OpenNewFishingHole();
        gameObject.SetActive(false);
        Debug.Log("Complete!");
    }

}
