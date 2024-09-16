using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoelTut : MonoBehaviour
{
    private AudioSource source;
    public AudioClip firstClip;
    public AudioClip secondClip;
    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            source.Play();

            GetComponent<Collider>().enabled = false;
        }
    }

    private void PlayFirst()
    {
        source.clip = firstClip;

        if(secondClip != null)
        {
            Invoke("PlaySecond", 5);
        }
       
    }

    private void PlaySecond()
    {
        source.clip = secondClip;
    }
}
