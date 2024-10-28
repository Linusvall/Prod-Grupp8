using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingHole : MonoBehaviour
{

    private bool canInteract = false;
    [SerializeField] private GameObject fishingGame;
    [SerializeField] private GameObject playerController;
    [SerializeField] private AudioClip[] fishingClips;
    [SerializeField] private AudioClip[] scaryClips;
    [SerializeField] private GameObject fishingHoleManager;
    [SerializeField] private float fadeDuration;
    private AudioSource source;

    // Start is called before the first frame update
    public void OnEnable()
    {
        source = GetComponent<AudioSource>();
        source.Play();
        StartCoroutine(FadeIn());
        fishingHoleManager.GetComponent<FishingHoleManager>().OpenNewFishingHole();
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
            StartFishing();
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

    private void StartFishing()
    {
        canInteract = false;
        fishingGame.GetComponent<FishingGame>().StartGame();
        playerController.GetComponent<PlayerController>().SetFishing(true);
        Debug.Log("StartFishing");
        source.Stop();
        gameObject.SetActive(false);
    }

}
