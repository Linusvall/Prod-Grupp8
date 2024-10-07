using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveAmbience : MonoBehaviour
{
    private AudioSource source;
    [SerializeField] private float fadeDuration;

    // Start is called before the first frame update
    void OnEnable()
    {
        source = GetComponent<AudioSource>();
        StartCoroutine(FadeIn());
    }

    private void OnDisable()
    {
        source.volume = 0f;
    }

    IEnumerator FadeIn()
    {
        float startVolume = source.volume;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            source.volume = Mathf.Lerp(startVolume, 0.5f, elapsedTime / fadeDuration);
            yield return null;
        }
    }

}
