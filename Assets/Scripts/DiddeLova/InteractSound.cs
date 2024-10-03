using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractSound : MonoBehaviour
{
    private AudioSource source;

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
        if(other.gameObject.layer == 6)
        {
            source.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            source.Pause();
        }
    }

    public void PauseInteractSound()
    {
        source.Pause();
    }

}
